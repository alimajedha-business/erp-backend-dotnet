using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class RemittanceNotFoundException()
    : NotFoundException("Remittance");

public sealed class RemittanceNumberAlreadyExistsException(long number)
    : DuplicateResourceException(number)
{
    public long Number { get; } = number;
    public override string LocalizationKey => "Remittance.Number.Duplicate";
}

public sealed class RemittanceHasRelatedEntitiesException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "Remittance.Delete.HasRelatedEntities";
}
