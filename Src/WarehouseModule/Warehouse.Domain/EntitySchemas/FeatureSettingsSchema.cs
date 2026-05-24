using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class FeatureSettingsSchema : IFilterSchema<FeatureSettings>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["maxCategoryLevel"] = new FilterFieldInfo(
            PropertyName: nameof(FeatureSettings.MaxCategoryLevel),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["warehouseSerialRule"] = new FilterFieldInfo(
            PropertyName: nameof(FeatureSettings.WarehouseSerialRule),
            PropertyType: typeof(WarehouseSerialRule),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "in", "notIn" }
        );

        filterSchema.Fields["priceRoundingPoint"] = new FilterFieldInfo(
            PropertyName: nameof(FeatureSettings.PriceRoundingPoint),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["createdAt"] = new FilterFieldInfo(
            PropertyName: nameof(FeatureSettings.CreatedAt),
            PropertyType: typeof(DateTime),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        return filterSchema;
    }
}
