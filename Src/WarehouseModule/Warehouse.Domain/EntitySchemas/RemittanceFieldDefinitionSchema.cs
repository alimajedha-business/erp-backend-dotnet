using NGErp.Base.Domain.EntitySchemas;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class RemittanceFieldDefinitionSchema :
    IFilterSchema<Entities.RemittanceFieldDefinition>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["key"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.RemittanceFieldDefinition.Key),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "contains", "startsWith", "endsWith" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.RemittanceFieldDefinition.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "contains", "startsWith", "endsWith" }
        );

        filterSchema.Fields["isActive"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.RemittanceFieldDefinition.IsActive),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
