using NGErp.Base.Domain.EntitySchemas;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class ReceiptFieldDefinitionSchema :
    IFilterSchema<Entities.ReceiptFieldDefinition>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["key"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptFieldDefinition.Key),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "contains", "startsWith", "endsWith" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptFieldDefinition.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "contains", "startsWith", "endsWith" }
        );

        filterSchema.Fields["isActive"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptFieldDefinition.IsActive),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
