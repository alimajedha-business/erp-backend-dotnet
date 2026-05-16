using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class JobCategorySchema : IFilterSchema<JobCategory>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(JobCategory.Code),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(JobCategory.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "contains", "startswith", "endswith" }
        );

        return filterSchema;
    }
}
