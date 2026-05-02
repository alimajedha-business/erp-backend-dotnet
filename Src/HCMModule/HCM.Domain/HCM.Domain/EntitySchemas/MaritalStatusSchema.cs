using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class MaritalStatusSchema : IFilterSchema<MaritalStatus>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(MaritalStatus.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne",
                "contains"
            }
        );

        filterSchema.Fields["type"] = new FilterFieldInfo(
            PropertyName: nameof(MaritalStatus.Type),
            PropertyType: typeof(MaritalStatusType),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne"
            }
        );

        return filterSchema;
    }
}
