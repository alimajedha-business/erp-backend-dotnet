using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptService(
    IAdvancedFilterBuilder filterBuilder,
    IReceiptRepository receiptRepository,
    IReceiptBusinessRuleValidator businessRuleValidator,
    IReceiptInventoryProjectionService receiptInventoryProjectionService,
    IValidator<CreateReceiptDto> createValidator,
    IValidator<PatchReceiptDto> patchValidator,
    IMapper mapper
) : IReceiptService
{
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IMapper _mapper = mapper;
    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly IReceiptBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IReceiptInventoryProjectionService _receiptInventoryProjectionService =
        receiptInventoryProjectionService;
    private readonly IValidator<CreateReceiptDto> _createValidator = createValidator;
    private readonly IValidator<PatchReceiptDto> _patchValidator = patchValidator;

    public async Task<ReceiptDto> CreateAsync(
        Guid companyId,
        CreateReceiptDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var receipt = new Receipt
        {
            CompanyId = companyId,
            Number = createDto.Number,
            ReceiptDate = createDto.ReceiptDate,
            ReceiptTypeId = createDto.ReceiptTypeId,
            Description = createDto.Description,
            Status = ReceiptStatus.Draft
        };

        AddHeaderFieldValues(companyId, receipt, createDto.ReceiptFieldValues);
        AddReceiptLines(companyId, receipt, createDto.ReceiptLines);

        var created = await _receiptRepository.AddAsync(receipt, ct);
        await _receiptRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(companyId, created.Id, trackChanges: false, ct);
    }

    public async Task<ReceiptDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var receipt = await _receiptRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges,
            ct
        );

        return receipt is not null
            ? _mapper.Map<ReceiptDto>(receipt)
            : throw new ReceiptNotFoundException();
    }

    public async Task<ListResponseModel<ReceiptListDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<Receipt>(filterNodeDto);
        var query = _receiptRepository.GetFiltered(companyId, advancedFilters);
        var res = await _receiptRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptListDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptListDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ReceiptDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchReceiptDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchReceiptPolicy.Validate(patchDocument);

        var receipt = await _receiptRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges: true,
            ct
        ) ?? throw new ReceiptNotFoundException();

        var patchDto = BuildPatchDto(receipt);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
            throw new InvalidPatchDocumentException(errors);

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);

        var updateDto = BuildCreateDto(patchDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, updateDto, ct);
        await _businessRuleValidator.ValidateUpdateAsync(companyId, id, updateDto, ct);

        var patchedPaths = patchDocument.Operations
            .Select(e => e.path.Trim('/').Split('/')[0])
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        receipt.Number = updateDto.Number;
        receipt.ReceiptDate = updateDto.ReceiptDate;
        receipt.ReceiptTypeId = updateDto.ReceiptTypeId;
        receipt.Description = updateDto.Description;

        if (patchedPaths.Contains(nameof(PatchReceiptDto.ReceiptFieldValues)))
        {
            ReplaceHeaderFieldValues(companyId, receipt, updateDto.ReceiptFieldValues);
        }

        if (patchedPaths.Contains(nameof(PatchReceiptDto.ReceiptLines)))
        {
            ReplaceReceiptLines(companyId, receipt, updateDto.ReceiptLines);
        }

        await _receiptRepository.SaveChangesAsync(ct);
        await _receiptInventoryProjectionService.RebuildAsync(companyId, receipt, ct);

        return await GetByIdAsync(companyId, id, trackChanges: false, ct);
    }

    public async Task<ReceiptDto> PostAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var receipt = await _receiptRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges: true,
            ct
        ) ?? throw new ReceiptNotFoundException();

        if (receipt.Status == ReceiptStatus.Posted)
            return await GetByIdAsync(companyId, id, trackChanges: false, ct);

        receipt.Status = ReceiptStatus.Posted;

        await _receiptRepository.SaveChangesAsync(ct);
        await _receiptInventoryProjectionService.RebuildAsync(companyId, receipt, ct);

        return await GetByIdAsync(companyId, id, trackChanges: false, ct);
    }

    public async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);
        await _receiptInventoryProjectionService.RemoveAsync(companyId, id, ct);
        await _receiptRepository.DeleteReceiptGraphAsync(companyId, id, ct);
    }

    public Task<int> GetNextNumber(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _receiptRepository.GetNextNumberAsync(companyId, ct);
    }

    private static void AddHeaderFieldValues(
        Guid companyId,
        Receipt receipt,
        IEnumerable<CreateReceiptFieldValueDto> fieldValues
    )
    {
        foreach (var fieldValueDto in fieldValues)
        {
            var fieldValue = MapReceiptFieldValue(companyId, fieldValueDto);

            fieldValue.Receipt = receipt;
            fieldValue.ReceiptLine = null;
            fieldValue.ReceiptLineId = null;

            receipt.ReceiptFieldValues.Add(fieldValue);
        }
    }

    private static void AddReceiptLines(
        Guid companyId,
        Receipt receipt,
        IEnumerable<CreateReceiptLineDto> lineDtos
    )
    {
        foreach (var lineDto in lineDtos)
        {
            var line = new ReceiptLine
            {
                CompanyId = companyId,
                RowNumber = lineDto.RowNumber,
                ItemId = lineDto.ItemId,
                WarehouseLocationId = lineDto.WarehouseLocationId,
                Weight = lineDto.Weight,
                Volume = lineDto.Volume,
                PreferredMassUnitId = lineDto.PreferredMassUnitId,
                PreferredVolumeUnitId = lineDto.PreferredVolumeUnitId,
                UnitPrice = lineDto.UnitPrice,
                TotalPrice = lineDto.TotalPrice,
                BatchNumber = lineDto.BatchNumber,
                SerialNumber = lineDto.SerialNumber,
                ExpiryDate = lineDto.ExpiryDate,
                Description = lineDto.Description,
                Receipt = receipt
            };

            foreach (var measurementValueDto in lineDto.ReceiptLineMeasurementValues)
            {
                line.ReceiptLineMeasurementValues.Add(
                    MapReceiptLineMeasurementValue(measurementValueDto, line)
                );
            }

            foreach (var attributeValueDto in lineDto.ReceiptLineAttributeValues)
            {
                var attributeValue = new ReceiptLineAttributeValue
                {
                    CompanyId = companyId,
                    ItemAttributeId = attributeValueDto.ItemAttributeId,
                    StringValue = attributeValueDto.StringValue,
                    DecimalValue = attributeValueDto.DecimalValue,
                    DateValue = attributeValueDto.DateValue,
                    DateTimeValue = attributeValueDto.DateTimeValue,
                    ReferenceId = attributeValueDto.ReferenceId,
                    BooleanValue = attributeValueDto.BooleanValue,
                    ReceiptLine = line
                };

                line.ReceiptLineAttributeValues.Add(attributeValue);
            }

            foreach (var fieldValueDto in lineDto.ReceiptFieldValues)
            {
                var fieldValue = MapReceiptFieldValue(companyId, fieldValueDto);

                fieldValue.Receipt = receipt;
                fieldValue.ReceiptLine = line;

                line.ReceiptFieldValues.Add(fieldValue);
                receipt.ReceiptFieldValues.Add(fieldValue);
            }

            receipt.ReceiptLines.Add(line);
        }
    }

    private void ReplaceHeaderFieldValues(
        Guid companyId,
        Receipt receipt,
        IReadOnlyCollection<CreateReceiptFieldValueDto> fieldValues
    )
    {
        var existingHeaderValues = receipt.ReceiptFieldValues
            .Where(e => e.ReceiptLineId is null)
            .ToList();

        _receiptRepository.RemoveReceiptFieldValues(existingHeaderValues);

        foreach (var fieldValue in existingHeaderValues)
        {
            receipt.ReceiptFieldValues.Remove(fieldValue);
        }

        AddHeaderFieldValues(companyId, receipt, fieldValues);
    }

    private void ReplaceReceiptLines(
        Guid companyId,
        Receipt receipt,
        IReadOnlyCollection<CreateReceiptLineDto> lineDtos
    )
    {
        var receiptLines = receipt.ReceiptLines.ToList();
        var lineFieldValues = receipt.ReceiptFieldValues
            .Where(e => e.ReceiptLineId is not null)
            .ToList();
        var lineAttributeValues = receiptLines
            .SelectMany(e => e.ReceiptLineAttributeValues)
            .ToList();
        var lineMeasurementValues = receiptLines
            .SelectMany(e => e.ReceiptLineMeasurementValues)
            .ToList();

        _receiptRepository.RemoveReceiptLineMeasurementValues(lineMeasurementValues);
        _receiptRepository.RemoveReceiptLineAttributeValues(lineAttributeValues);
        _receiptRepository.RemoveReceiptFieldValues(lineFieldValues);
        _receiptRepository.RemoveReceiptLines(receiptLines);

        foreach (var fieldValue in lineFieldValues)
        {
            receipt.ReceiptFieldValues.Remove(fieldValue);
        }

        foreach (var line in receiptLines)
        {
            receipt.ReceiptLines.Remove(line);
        }

        AddReceiptLines(companyId, receipt, lineDtos);
    }

    private static ReceiptFieldValue MapReceiptFieldValue(
        Guid companyId,
        CreateReceiptFieldValueDto dto
    )
    {
        return new ReceiptFieldValue
        {
            CompanyId = companyId,
            FieldDefinitionId = dto.FieldDefinitionId,
            StringValue = dto.StringValue,
            IntValue = dto.IntValue,
            DecimalValue = dto.DecimalValue,
            DateValue = dto.DateValue,
            DateTimeValue = dto.DateTimeValue,
            ReferenceId = dto.ReferenceId,
            BooleanValue = dto.BooleanValue
        };
    }

    private static PatchReceiptDto BuildPatchDto(Receipt receipt)
    {
        return new PatchReceiptDto
        {
            Number = receipt.Number,
            ReceiptDate = receipt.ReceiptDate,
            ReceiptTypeId = receipt.ReceiptTypeId,
            Description = receipt.Description,
            ReceiptFieldValues = [.. receipt.ReceiptFieldValues
                .Where(e => e.ReceiptLineId is null)
                .Select(MapCreateReceiptFieldValueDto)],
            ReceiptLines = [.. receipt.ReceiptLines
                .OrderBy(e => e.RowNumber)
                .Select(MapCreateReceiptLineDto)]
        };
    }

    private static CreateReceiptDto BuildCreateDto(PatchReceiptDto patchDto)
    {
        return new CreateReceiptDto
        {
            Number = patchDto.Number!.Value,
            ReceiptDate = patchDto.ReceiptDate!.Value,
            ReceiptTypeId = patchDto.ReceiptTypeId!.Value,
            Description = patchDto.Description,
            ReceiptFieldValues = patchDto.ReceiptFieldValues ?? [],
            ReceiptLines = patchDto.ReceiptLines ?? []
        };
    }

    private static CreateReceiptLineDto MapCreateReceiptLineDto(ReceiptLine line)
    {
        return new CreateReceiptLineDto
        {
            RowNumber = line.RowNumber,
            ItemId = line.ItemId,
            WarehouseLocationId = line.WarehouseLocationId,
            Weight = line.Weight,
            Volume = line.Volume,
            PreferredMassUnitId = line.PreferredMassUnitId,
            PreferredVolumeUnitId = line.PreferredVolumeUnitId,
            UnitPrice = line.UnitPrice,
            TotalPrice = line.TotalPrice,
            BatchNumber = line.BatchNumber,
            SerialNumber = line.SerialNumber,
            ExpiryDate = line.ExpiryDate,
            Description = line.Description,
            ReceiptLineMeasurementValues = [.. line.ReceiptLineMeasurementValues
                .Select(MapCreateReceiptLineMeasurementValueDto)],
            ReceiptLineAttributeValues = [.. line.ReceiptLineAttributeValues
                .Select(MapCreateReceiptLineAttributeValueDto)],
            ReceiptFieldValues = [.. line.ReceiptFieldValues
                .Select(MapCreateReceiptFieldValueDto)]
        };
    }

    private static CreateReceiptFieldValueDto MapCreateReceiptFieldValueDto(
        ReceiptFieldValue fieldValue
    )
    {
        return new CreateReceiptFieldValueDto
        {
            FieldDefinitionId = fieldValue.FieldDefinitionId,
            StringValue = fieldValue.StringValue,
            IntValue = fieldValue.IntValue,
            DecimalValue = fieldValue.DecimalValue,
            DateValue = fieldValue.DateValue,
            DateTimeValue = fieldValue.DateTimeValue,
            ReferenceId = fieldValue.ReferenceId,
            BooleanValue = fieldValue.BooleanValue
        };
    }

    private static CreateReceiptLineAttributeValueDto MapCreateReceiptLineAttributeValueDto(
        ReceiptLineAttributeValue attributeValue
    )
    {
        return new CreateReceiptLineAttributeValueDto
        {
            ItemAttributeId = attributeValue.ItemAttributeId,
            StringValue = attributeValue.StringValue,
            DecimalValue = attributeValue.DecimalValue,
            DateValue = attributeValue.DateValue,
            DateTimeValue = attributeValue.DateTimeValue,
            ReferenceId = attributeValue.ReferenceId,
            BooleanValue = attributeValue.BooleanValue
        };
    }

    private static ReceiptLineMeasurementValue MapReceiptLineMeasurementValue(
        CreateReceiptLineMeasurementValueDto dto,
        ReceiptLine line
    )
    {
        return new ReceiptLineMeasurementValue
        {
            ReceiptLine = line,
            ItemUnitOfMeasurementId = dto.ItemUnitOfMeasurementId,
            Quantity = dto.Quantity
        };
    }

    private static CreateReceiptLineMeasurementValueDto MapCreateReceiptLineMeasurementValueDto(
        ReceiptLineMeasurementValue measurementValue
    )
    {
        return new CreateReceiptLineMeasurementValueDto
        {
            ItemUnitOfMeasurementId = measurementValue.ItemUnitOfMeasurementId,
            Quantity = measurementValue.Quantity
        };
    }
}
