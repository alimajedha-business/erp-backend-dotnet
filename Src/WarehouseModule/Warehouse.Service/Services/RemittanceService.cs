using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class RemittanceService(
    IAdvancedFilterBuilder filterBuilder,
    IRemittanceRepository remittanceRepository,
    IRemittanceBusinessRuleValidator businessRuleValidator,
    IRemittanceInventoryProjectionService remittanceInventoryProjectionService,
    IMapper mapper
) : IRemittanceService
{
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IMapper _mapper = mapper;
    private readonly IRemittanceRepository _remittanceRepository = remittanceRepository;
    private readonly IRemittanceBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IRemittanceInventoryProjectionService _remittanceInventoryProjectionService = remittanceInventoryProjectionService;

    public async Task<RemittanceDto> CreateAsync(Guid companyId, CreateRemittanceDto createDto, CancellationToken ct)
    {
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var remittance = new Remittance
        {
            CompanyId = companyId,
            Number = createDto.Number,
            RemittanceDate = createDto.RemittanceDate,
            RemittanceTypeId = createDto.RemittanceTypeId,
            Description = createDto.Description,
            Status = RemittanceStatus.Draft
        };

        AddHeaderFieldValues(companyId, remittance, createDto.RemittanceFieldValues);
        AddRemittanceLines(companyId, remittance, createDto.RemittanceLines);

        var created = await _remittanceRepository.AddAsync(remittance, ct);
        await _remittanceRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(companyId, created.Id, trackChanges: false, ct);
    }

    public async Task<RemittanceDto> GetByIdAsync(Guid companyId, Guid id, bool trackChanges = false, CancellationToken ct = default)
    {
        var remittance = await _remittanceRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges,
            ct
        );

        return remittance is not null
            ? _mapper.Map<RemittanceDto>(remittance)
            : throw new RemittanceNotFoundException();
    }

    public async Task<ListResponseModel<RemittanceListDto>> GetFilteredAsync(
        Guid companyId,
        RemittanceParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<Remittance>(filterNodeDto);
        var query = _remittanceRepository.GetFiltered(companyId, advancedFilters);
        var res = await _remittanceRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<RemittanceListDto>(
            results: _mapper.Map<IReadOnlyList<RemittanceListDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<RemittanceDto> PatchAsync(Guid companyId, Guid id, JsonPatchDocument<PatchRemittanceDto> patchDocument, CancellationToken ct)
    {
        var remittance = await _remittanceRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges: true,
            ct
        ) ?? throw new RemittanceNotFoundException();

        var patchDto = BuildPatchDto(remittance);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error => errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}"));

        if (errors.Count != 0)
            throw new InvalidPatchDocumentException(errors);

        var updateDto = BuildCreateDto(patchDto);
        await _businessRuleValidator.ValidateUpdateAsync(companyId, id, updateDto, ct);

        var patchedPaths = patchDocument.Operations
            .Select(e => e.path.Trim('/').Split('/')[0])
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        remittance.Number = updateDto.Number;
        remittance.RemittanceDate = updateDto.RemittanceDate;
        remittance.RemittanceTypeId = updateDto.RemittanceTypeId;
        remittance.Description = updateDto.Description;

        if (patchedPaths.Contains(nameof(PatchRemittanceDto.RemittanceFieldValues)))
            ReplaceHeaderFieldValues(companyId, remittance, updateDto.RemittanceFieldValues);

        if (patchedPaths.Contains(nameof(PatchRemittanceDto.RemittanceLines)))
            ReplaceRemittanceLines(companyId, remittance, updateDto.RemittanceLines);

        await _remittanceRepository.SaveChangesAsync(ct);
        await _remittanceInventoryProjectionService.RebuildAsync(companyId, remittance, ct);

        return await GetByIdAsync(companyId, id, trackChanges: false, ct);
    }

    public async Task<RemittanceDto> PostAsync(Guid companyId, Guid id, CancellationToken ct)
    {
        var remittance = await _remittanceRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            trackChanges: true,
            ct
        ) ?? throw new RemittanceNotFoundException();

        if (remittance.Status == RemittanceStatus.Posted)
            return await GetByIdAsync(companyId, id, trackChanges: false, ct);

        remittance.Status = RemittanceStatus.Posted;
        await _remittanceRepository.SaveChangesAsync(ct);
        await _remittanceInventoryProjectionService.RebuildAsync(companyId, remittance, ct);

        return await GetByIdAsync(companyId, id, trackChanges: false, ct);
    }

    public async Task DeleteAsync(Guid companyId, Guid id, CancellationToken ct)
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);
        await _remittanceInventoryProjectionService.RemoveAsync(companyId, id, ct);
        await _remittanceRepository.DeleteRemittanceGraphAsync(companyId, id, ct);
    }

    public Task<int> GetNextNumber(Guid companyId, CancellationToken ct)
    {
        return _remittanceRepository.GetNextNumberAsync(companyId, ct);
    }

    private static void AddHeaderFieldValues(Guid companyId, Remittance remittance, IEnumerable<CreateRemittanceFieldValueDto> fieldValues)
    {
        foreach (var fieldValueDto in fieldValues)
        {
            var fieldValue = MapRemittanceFieldValue(companyId, fieldValueDto);
            fieldValue.Remittance = remittance;
            fieldValue.RemittanceLine = null;
            fieldValue.RemittanceLineId = null;
            remittance.RemittanceFieldValues.Add(fieldValue);
        }
    }

    private static void AddRemittanceLines(Guid companyId, Remittance remittance, IEnumerable<CreateRemittanceLineDto> lineDtos)
    {
        foreach (var lineDto in lineDtos)
        {
            var line = new RemittanceLine
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
                Remittance = remittance
            };

            foreach (var measurementValueDto in lineDto.RemittanceLineMeasurementValues)
            {
                line.RemittanceLineMeasurementValues.Add(new RemittanceLineMeasurementValue
                {
                    CompanyId = companyId,
                    RemittanceLine = line,
                    ItemUnitOfMeasurementId = measurementValueDto.ItemUnitOfMeasurementId,
                    Quantity = measurementValueDto.Quantity
                });
            }

            foreach (var attributeValueDto in lineDto.RemittanceLineAttributeValues)
            {
                line.RemittanceLineAttributeValues.Add(new RemittanceLineAttributeValue
                {
                    CompanyId = companyId,
                    ItemAttributeId = attributeValueDto.ItemAttributeId,
                    StringValue = attributeValueDto.StringValue,
                    DecimalValue = attributeValueDto.DecimalValue,
                    DateValue = attributeValueDto.DateValue,
                    DateTimeValue = attributeValueDto.DateTimeValue,
                    ReferenceId = attributeValueDto.ReferenceId,
                    BooleanValue = attributeValueDto.BooleanValue,
                    RemittanceLine = line
                });
            }

            foreach (var fieldValueDto in lineDto.RemittanceFieldValues)
            {
                var fieldValue = MapRemittanceFieldValue(companyId, fieldValueDto);
                fieldValue.Remittance = remittance;
                fieldValue.RemittanceLine = line;
                line.RemittanceFieldValues.Add(fieldValue);
                remittance.RemittanceFieldValues.Add(fieldValue);
            }

            remittance.RemittanceLines.Add(line);
        }
    }

    private void ReplaceHeaderFieldValues(Guid companyId, Remittance remittance, IReadOnlyCollection<CreateRemittanceFieldValueDto> fieldValues)
    {
        var existingHeaderValues = remittance.RemittanceFieldValues.Where(e => e.RemittanceLineId is null).ToList();
        _remittanceRepository.RemoveRemittanceFieldValues(existingHeaderValues);
        foreach (var fieldValue in existingHeaderValues)
            remittance.RemittanceFieldValues.Remove(fieldValue);
        AddHeaderFieldValues(companyId, remittance, fieldValues);
    }

    private void ReplaceRemittanceLines(Guid companyId, Remittance remittance, IReadOnlyCollection<CreateRemittanceLineDto> lineDtos)
    {
        var remittanceLines = remittance.RemittanceLines.ToList();
        var lineFieldValues = remittance.RemittanceFieldValues.Where(e => e.RemittanceLineId is not null).ToList();
        var lineAttributeValues = remittanceLines.SelectMany(e => e.RemittanceLineAttributeValues).ToList();
        var lineMeasurementValues = remittanceLines.SelectMany(e => e.RemittanceLineMeasurementValues).ToList();

        _remittanceRepository.RemoveRemittanceLineMeasurementValues(lineMeasurementValues);
        _remittanceRepository.RemoveRemittanceLineAttributeValues(lineAttributeValues);
        _remittanceRepository.RemoveRemittanceFieldValues(lineFieldValues);
        _remittanceRepository.RemoveRemittanceLines(remittanceLines);

        foreach (var fieldValue in lineFieldValues)
            remittance.RemittanceFieldValues.Remove(fieldValue);
        foreach (var line in remittanceLines)
            remittance.RemittanceLines.Remove(line);

        AddRemittanceLines(companyId, remittance, lineDtos);
    }

    private static RemittanceFieldValue MapRemittanceFieldValue(Guid companyId, CreateRemittanceFieldValueDto dto)
    {
        return new RemittanceFieldValue
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

    private static PatchRemittanceDto BuildPatchDto(Remittance remittance)
    {
        return new PatchRemittanceDto
        {
            Number = remittance.Number,
            RemittanceDate = remittance.RemittanceDate,
            RemittanceTypeId = remittance.RemittanceTypeId,
            Description = remittance.Description,
            RemittanceFieldValues = [.. remittance.RemittanceFieldValues.Where(e => e.RemittanceLineId is null).Select(MapCreateRemittanceFieldValueDto)],
            RemittanceLines = [.. remittance.RemittanceLines.OrderBy(e => e.RowNumber).Select(MapCreateRemittanceLineDto)]
        };
    }

    private static CreateRemittanceDto BuildCreateDto(PatchRemittanceDto patchDto)
    {
        return new CreateRemittanceDto
        {
            Number = patchDto.Number!.Value,
            RemittanceDate = patchDto.RemittanceDate!.Value,
            RemittanceTypeId = patchDto.RemittanceTypeId!.Value,
            Description = patchDto.Description,
            RemittanceFieldValues = patchDto.RemittanceFieldValues ?? [],
            RemittanceLines = patchDto.RemittanceLines ?? []
        };
    }

    private static CreateRemittanceLineDto MapCreateRemittanceLineDto(RemittanceLine line)
    {
        return new CreateRemittanceLineDto
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
            RemittanceLineMeasurementValues = [.. line.RemittanceLineMeasurementValues.Select(e => new CreateRemittanceLineMeasurementValueDto { ItemUnitOfMeasurementId = e.ItemUnitOfMeasurementId, Quantity = e.Quantity })],
            RemittanceLineAttributeValues = [.. line.RemittanceLineAttributeValues.Select(MapCreateRemittanceLineAttributeValueDto)],
            RemittanceFieldValues = [.. line.RemittanceFieldValues.Select(MapCreateRemittanceFieldValueDto)]
        };
    }

    private static CreateRemittanceFieldValueDto MapCreateRemittanceFieldValueDto(RemittanceFieldValue fieldValue)
    {
        return new CreateRemittanceFieldValueDto
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

    private static CreateRemittanceLineAttributeValueDto MapCreateRemittanceLineAttributeValueDto(RemittanceLineAttributeValue attributeValue)
    {
        return new CreateRemittanceLineAttributeValueDto
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
}
