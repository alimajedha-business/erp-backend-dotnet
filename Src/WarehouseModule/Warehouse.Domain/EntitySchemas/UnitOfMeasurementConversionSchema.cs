using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class UnitOfMeasurementConversionSchema :
    IFilterSchema<UnitOfMeasurementConversion>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();
        filterSchema.Fields["fromUnitOfMeasurementId"] = new FilterFieldInfo(
            PropertyName: nameof(UnitOfMeasurementConversion.FromUnitOfMeasurementId),
            PropertyType: typeof(Guid),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );
        filterSchema.Fields["toUnitOfMeasurementId"] = new FilterFieldInfo(
            PropertyName: nameof(UnitOfMeasurementConversion.ToUnitOfMeasurementId),
            PropertyType: typeof(Guid),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
