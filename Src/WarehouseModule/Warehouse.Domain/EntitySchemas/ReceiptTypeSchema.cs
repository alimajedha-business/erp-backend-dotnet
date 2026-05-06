using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class ReceiptTypeSchema : IFilterSchema<ReceiptType>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(ReceiptType.Code),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(ReceiptType.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        filterSchema.Fields["addToStock"] = new FilterFieldInfo(
            PropertyName: nameof(ReceiptType.AddToStock),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
