using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptNotFoundException()
    : NotFoundException("Receipt");

public sealed class ReceiptNumberAlreadyExistsException(long number)
    : DuplicateResourceException(number)
{
    public long Number { get; } = number;
    public override string LocalizationKey => "Receipt.Number.Duplicate";
}

public sealed class ReceiptDuplicateRowNumberException(int rowNumber)
    : BusinessRuleViolationException(rowNumber)
{
    public int RowNumber { get; } = rowNumber;
    public override string LocalizationKey => "Receipt.ReceiptLines.RowNumber.Duplicate";
}

public sealed class ReceiptDuplicateFieldDefinitionException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey => "Receipt.FieldValue.FieldDefinition.Duplicate";
}

public sealed class ReceiptDuplicateLineAttributeException(Guid itemAttributeId)
    : BusinessRuleViolationException(itemAttributeId)
{
    public Guid ItemAttributeId { get; } = itemAttributeId;
    public override string LocalizationKey => "Receipt.LineAttributeValue.ItemAttribute.Duplicate";
}

public sealed class ReceiptFieldDefinitionNotConfiguredException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey => "Receipt.FieldValue.FieldDefinition.NotConfigured";
}

public sealed class ReceiptRequiredFieldValueMissingException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey => "Receipt.FieldValue.Required";
}

public sealed class ReceiptFieldValueDataTypeMismatchException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey => "Receipt.FieldValue.DataTypeMismatch";
}

public sealed class ReceiptLineUnitOfMeasurementNotAllowedException(Guid unitOfMeasurementId)
    : BusinessRuleViolationException(unitOfMeasurementId)
{
    public Guid UnitOfMeasurementId { get; } = unitOfMeasurementId;
    public override string LocalizationKey => "ReceiptLine.UnitOfMeasurement.NotAllowed";
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
