using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptTypeNotFoundException()
    : NotFoundException("ReceiptType");

public sealed class ReceiptTypeCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "ReceiptType.Code.Duplicate";
}

public sealed class ReceiptTypeHasRelatedEntitiesException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "ReceiptType.Delete.HasRelatedEntities";
}
