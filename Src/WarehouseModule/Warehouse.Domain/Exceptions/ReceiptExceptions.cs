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

public sealed class ReceiptDuplicateLineMeasurementValueException(Guid itemUnitOfMeasurementId)
    : BusinessRuleViolationException(itemUnitOfMeasurementId)
{
    public Guid ItemUnitOfMeasurementId { get; } = itemUnitOfMeasurementId;
    public override string LocalizationKey => "Receipt.LineMeasurementValue.ItemUnitOfMeasurement.Duplicate";
}

public sealed class ReceiptLineValidationException(
    IReadOnlyDictionary<int, IReadOnlyList<Exception>> errors
) : ListValidationException(errors)
{
    public override string LocalizationKey => "Receipt.Line.ValidationFailed";
}

public sealed class ReceiptFieldDefinitionNotConfiguredException :
    BusinessRuleViolationException
{
    public ReceiptFieldDefinitionNotConfiguredException(
        Guid fieldDefinitionId,
        string placement
    ) : base(fieldDefinitionId, placement)
    {
        FieldDefinitionId = fieldDefinitionId;
        Placement = placement;
    }

    public ReceiptFieldDefinitionNotConfiguredException(
        Guid fieldDefinitionId,
        string fieldDefinitionTitle,
        string placement
    ) : base(fieldDefinitionTitle, placement)
    {
        FieldDefinitionId = fieldDefinitionId;
        FieldDefinitionTitle = fieldDefinitionTitle;
        Placement = placement;
    }

    public Guid FieldDefinitionId { get; }
    public string? FieldDefinitionTitle { get; }
    public string Placement { get; }
    public override string LocalizationKey => "Receipt.FieldValue.FieldDefinition.NotConfigured";
}

public sealed class ReceiptRequiredFieldValueMissingException(
    Guid fieldDefinitionId,
    string fieldDefinitionTitle
) : BusinessRuleViolationException(fieldDefinitionTitle)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public string FieldDefinitionTitle { get; } = fieldDefinitionTitle;
    public override string LocalizationKey => "Receipt.FieldValue.Required";
}

public sealed class ReceiptFieldValueDataTypeMismatchException(
    Guid fieldDefinitionId,
    string fieldDefinitionTitle,
    string dataType
) : BusinessRuleViolationException(fieldDefinitionTitle, dataType)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public string FieldDefinitionTitle { get; } = fieldDefinitionTitle;
    public string DataType { get; } = dataType;
    public override string LocalizationKey => "Receipt.FieldValue.DataTypeMismatch";
}

public sealed class ReceiptLineUnitOfMeasurementNotAllowedException(
    int rowNumber,
    Guid unitOfMeasurementId
) : BusinessRuleViolationException(unitOfMeasurementId)
{
    public int RowNumber { get; } = rowNumber;
    public Guid UnitOfMeasurementId { get; } = unitOfMeasurementId;
    public override string LocalizationKey => "ReceiptLine.UnitOfMeasurement.NotAllowed";
}

public sealed class ReceiptLinePreferredUnitNotFoundException(
    int rowNumber,
    Guid preferredUnitId
) : BusinessRuleViolationException(preferredUnitId)
{
    public int RowNumber { get; } = rowNumber;
    public Guid PreferredUnitId { get; } = preferredUnitId;
    public override string LocalizationKey => "ReceiptLine.PreferredUnit.NotFound";
}

public sealed class ReceiptLinePreferredUnitDimensionMismatchException(
    int rowNumber,
    Guid preferredUnitId,
    string expectedDimension
) : BusinessRuleViolationException(preferredUnitId, expectedDimension)
{
    public int RowNumber { get; } = rowNumber;
    public Guid PreferredUnitId { get; } = preferredUnitId;
    public string ExpectedDimension { get; } = expectedDimension;
    public override string LocalizationKey => "ReceiptLine.PreferredUnit.DimensionMismatch";
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
