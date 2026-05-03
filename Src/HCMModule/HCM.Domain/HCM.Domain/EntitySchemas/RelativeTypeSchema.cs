using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class RelativeTypeSchema : IFilterSchema<RelativeType>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(RelativeType.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne",
                "contains"
            }
        );

        filterSchema.Fields["type"] = new FilterFieldInfo(
            PropertyName: nameof(RelativeType.Type),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne"
            }
        );

        return filterSchema;
    }
}
