using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptTypeConfigurationNotFoundException()
    : NotFoundException("ReceiptTypeConfiguration");

public sealed class ReceiptTypeConfigurationAlreadyExistsException(Guid receiptTypeId)
    : DuplicateResourceException(receiptTypeId)
{
    public Guid ReceiptTypeId { get; } = receiptTypeId;
    public override string LocalizationKey => "ReceiptTypeConfiguration.ReceiptType.Duplicate";
}

public sealed class ReceiptTypeConfigurationFieldDefinitionDuplicateException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey =>
        "ReceiptTypeConfiguration.FieldConfigurations.FieldDefinition.Duplicate";
}
