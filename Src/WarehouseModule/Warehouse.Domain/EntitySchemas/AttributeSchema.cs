using NGErp.Base.Domain.EntitySchemas;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public class AttributeSchema : IFilterSchema<Entities.Attribute>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.Attribute.Code),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        return filterSchema;
    }
}
