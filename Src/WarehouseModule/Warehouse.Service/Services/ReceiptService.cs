using AutoMapper;

using FluentValidation;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptService(
    IReceiptRepository receiptRepository,
    IReceiptBusinessRuleValidator businessRuleValidator,
    IValidator<CreateReceiptDto> createValidator,
    IMapper mapper
) : IReceiptService
{
    private readonly IMapper _mapper = mapper;
    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly IReceiptBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateReceiptDto> _createValidator = createValidator;

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
            Number = createDto.Number.Trim(),
            ReceiptDate = createDto.ReceiptDate,
            ReceiptTypeId = createDto.ReceiptTypeId,
            Description = createDto.Description
        };

        AddHeaderFieldValues(companyId, receipt, createDto.ReceiptFieldValues);
        AddReceiptLines(companyId, receipt, createDto.ReceiptLines);

        var created = await _receiptRepository.AddAsync(receipt, ct);
        await _receiptRepository.SaveChangesAsync(ct);

        return _mapper.Map<ReceiptDto>(created);
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
                UnitOfMeasurementId = lineDto.UnitOfMeasurementId,
                Quantity = lineDto.Quantity,
                UnitPrice = lineDto.UnitPrice,
                TotalPrice = lineDto.TotalPrice,
                Receipt = receipt
            };

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
}
