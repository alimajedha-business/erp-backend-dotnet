using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class WorkLocationSchema : IFilterSchema<WorkLocation>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["parentId"] = new FilterFieldInfo(
            PropertyName: nameof(WorkLocation.ParentId),
            PropertyType: typeof(Guid?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(WorkLocation.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "contains", "startswith", "endswith" }
        );

        return filterSchema;
    }
}
