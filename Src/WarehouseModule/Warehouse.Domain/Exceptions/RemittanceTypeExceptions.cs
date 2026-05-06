using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class RemittanceTypeNotFoundException()
    : NotFoundException("RemittanceType");

public sealed class RemittanceTypeCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "RemittanceType.Code.Duplicate";
}

public sealed class RemittanceTypeHasRelatedEntitiesException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "RemittanceType.Delete.HasRelatedEntities";
}
