using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class UnitOfMeasurementHasItemsException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "UnitOfMeasurement.Delete.HasItems";
}
