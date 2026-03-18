using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class WarehouseSchema : IFilterSchema<Entities.Warehouse>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.Warehouse.Code),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "gte", "lt", "lte" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.Warehouse.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        filterSchema.Fields["maxMonetaryValue"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.Warehouse.MaxMonetaryValue),
            PropertyType: typeof(decimal),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "gte", "lt", "lte" }
        );

        filterSchema.Fields["isActive"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.Warehouse.IsActive),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
