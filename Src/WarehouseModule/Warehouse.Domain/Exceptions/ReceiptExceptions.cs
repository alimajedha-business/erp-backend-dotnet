using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptNotFoundException()
    : NotFoundException("Receipt");

public sealed class ReceiptDuplicateRowNumberException(int rowNumber)
    : BusinessRuleViolationException(rowNumber)
{
    public int RowNumber { get; } = rowNumber;
    public override string LocalizationKey => "Receipt.ReceiptLines.RowNumber.Duplicate";
}

public sealed class ReceiptFieldValueMustHaveExactlyOneValueException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey => "Receipt.FieldValue.ExactlyOneValue";
}

public sealed class ReceiptLineAttributeValueMustHaveExactlyOneValueException(
    Guid itemAttributeId
) : BusinessRuleViolationException(itemAttributeId)
{
    public Guid ItemAttributeId { get; } = itemAttributeId;
    public override string LocalizationKey => "Receipt.LineAttributeValue.ExactlyOneValue";
}
