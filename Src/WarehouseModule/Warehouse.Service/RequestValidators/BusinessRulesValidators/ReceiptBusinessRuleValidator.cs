using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ReceiptBusinessRuleValidator(
    IReceiptTypeService receiptTypeService
) : IReceiptBusinessRuleValidator
{
    private readonly IReceiptTypeService _receiptTypeService = receiptTypeService;

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptDto createDto,
        CancellationToken ct
    )
    {
        await _receiptTypeService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: e => e.CompanyId == companyId && e.Id == createDto.ReceiptTypeId,
            ct
        );

        ValidateDuplicateRows(createDto);
        ValidateValueObjects(createDto);
    }

    private static void ValidateDuplicateRows(CreateReceiptDto createDto)
    {
        var duplicate = createDto.ReceiptLines
            .GroupBy(e => e.RowNumber)
            .FirstOrDefault(e => e.Count() > 1);

        if (duplicate is not null)
            throw new ReceiptDuplicateRowNumberException(duplicate.Key);
    }

    private static void ValidateValueObjects(CreateReceiptDto createDto)
    {
        foreach (var headerValue in createDto.ReceiptFieldValues)
            ValidateOnlyOneValueIsFilled(headerValue);

        foreach (var line in createDto.ReceiptLines)
        {
            foreach (var attributeValue in line.ReceiptLineAttributeValues)
                ValidateOnlyOneValueIsFilled(attributeValue);

            foreach (var fieldValue in line.ReceiptFieldValues)
                ValidateOnlyOneValueIsFilled(fieldValue);
        }
    }

    private static void ValidateOnlyOneValueIsFilled(CreateReceiptFieldValueDto dto)
    {
        var filledCount = new object?[]
        {
            dto.StringValue,
            dto.IntValue,
            dto.DecimalValue,
            dto.DateValue,
            dto.DateTimeValue,
            dto.ReferenceId,
            dto.BooleanValue
        }.Count(e => e is not null);

        if (filledCount != 1)
            throw new ReceiptFieldValueMustHaveExactlyOneValueException(dto.FieldDefinitionId);
    }

    private static void ValidateOnlyOneValueIsFilled(CreateReceiptLineAttributeValueDto dto)
    {
        var filledCount = new object?[]
        {
            dto.StringValue,
            dto.DecimalValue,
            dto.DateValue,
            dto.DateTimeValue,
            dto.ReferenceId,
            dto.BooleanValue
        }.Count(e => e is not null);

        if (filledCount != 1)
            throw new ReceiptLineAttributeValueMustHaveExactlyOneValueException(
                dto.ItemAttributeId
            );
    }
}
