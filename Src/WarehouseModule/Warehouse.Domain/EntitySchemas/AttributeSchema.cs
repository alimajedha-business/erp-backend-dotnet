using NGErp.Base.Domain.EntitySchemas;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class AttributeSchema : IFilterSchema<Entities.Attribute>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.Attribute.Code),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        return filterSchema;
    }
}
