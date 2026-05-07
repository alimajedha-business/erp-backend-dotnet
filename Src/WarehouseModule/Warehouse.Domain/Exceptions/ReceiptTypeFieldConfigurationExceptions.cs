using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptTypeFieldConfigurationNotFoundException()
    : NotFoundException("ReceiptTypeFieldConfiguration");

public sealed class ReceiptTypeFieldConfigurationPlacementNotAllowedException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey =>
        "ReceiptTypeFieldConfiguration.Placement.NotAllowed";
}

public sealed class ReceiptTypeFieldConfigurationRequiredWithoutExistsException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey =>
        "ReceiptTypeFieldConfiguration.IsRequired.ExistsFalse";
}
