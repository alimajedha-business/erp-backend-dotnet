using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public class WarehouseLocationSchema : IFilterSchema<WarehouseLocation>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Code),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "gte", "lt", "lte" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        filterSchema.Fields["canStoreItem"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.CanStoreItem),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
