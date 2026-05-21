using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class MilitaryServiceStatusSchema : IFilterSchema<MilitaryServiceStatus>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(MilitaryServiceStatus.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne",
                "contains"
            }
        );

        filterSchema.Fields["type"] = new FilterFieldInfo(
            PropertyName: nameof(MilitaryServiceStatus.Type),
            PropertyType: typeof(MilitaryStatusType),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne"
            }
        );

        return filterSchema;
    }
}
