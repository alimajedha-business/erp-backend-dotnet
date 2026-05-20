using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class RemittanceTypeFieldConfigurationNotFoundException()
    : NotFoundException("RemittanceTypeFieldConfiguration");

public sealed class RemittanceTypeFieldConfigurationPlacementNotAllowedException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey =>
        "RemittanceTypeFieldConfiguration.Placement.NotAllowed";
}

public sealed class RemittanceTypeFieldConfigurationRequiredWithoutExistsException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey =>
        "RemittanceTypeFieldConfiguration.IsRequired.ExistsFalse";
}
