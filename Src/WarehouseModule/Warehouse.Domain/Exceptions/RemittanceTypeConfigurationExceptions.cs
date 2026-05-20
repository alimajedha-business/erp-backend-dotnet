using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class RemittanceTypeConfigurationNotFoundException()
    : NotFoundException("RemittanceTypeConfiguration");

public sealed class RemittanceTypeConfigurationAlreadyExistsException(Guid remittanceTypeId)
    : DuplicateResourceException(remittanceTypeId)
{
    public Guid RemittanceTypeId { get; } = remittanceTypeId;
    public override string LocalizationKey => "RemittanceTypeConfiguration.RemittanceType.Duplicate";
}

public sealed class RemittanceTypeConfigurationFieldDefinitionDuplicateException(Guid fieldDefinitionId)
    : BusinessRuleViolationException(fieldDefinitionId)
{
    public Guid FieldDefinitionId { get; } = fieldDefinitionId;
    public override string LocalizationKey =>
        "RemittanceTypeConfiguration.FieldConfigurations.FieldDefinition.Duplicate";
}
