using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptSourceOfSupplyNotFoundException()
    : NotFoundException("ReceiptSourceOfSupply");

public sealed class ReceiptSourceOfSupplyCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "ReceiptSourceOfSupply.Code.Duplicate";
}

public sealed class ReceiptSourceOfSupplyHasRelatedEntitiesException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "ReceiptSourceOfSupply.Delete.HasRelatedEntities";
}
