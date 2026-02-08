using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public class AttributeEnumValueSchema : IFilterSchema<AttributeEnumValue>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(AttributeEnumValue.Code),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        return filterSchema;
    }
}
