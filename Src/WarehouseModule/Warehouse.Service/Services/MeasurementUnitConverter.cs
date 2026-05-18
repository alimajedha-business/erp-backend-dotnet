using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Services;

public static class MeasurementUnitConverter
{
    public static decimal? ConvertToBase(
        decimal? value,
        Guid? unitId,
        IReadOnlyDictionary<Guid, SiUnit> unitsById
    )
    {
        if (!value.HasValue)
            return null;

        if (!unitId.HasValue || !unitsById.TryGetValue(unitId.Value, out var unit))
            return value;

        return ConvertToBase(value, unit);
    }

    public static decimal? ConvertToBase(decimal? value, SiUnit? preferredUnit)
    {
        if (!value.HasValue || preferredUnit is null)
            return value;

        return value.Value * preferredUnit.FactorToBase;
    }

    public static decimal? ConvertFromBase(decimal? value, SiUnit? preferredUnit)
    {
        if (!value.HasValue || preferredUnit is null)
            return value;

        if (preferredUnit.FactorToBase == 0)
            return value;

        return value.Value / preferredUnit.FactorToBase;
    }
}
