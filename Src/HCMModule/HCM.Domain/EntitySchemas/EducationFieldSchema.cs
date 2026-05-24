using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class EducationFieldSchema : IFilterSchema<EducationField>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(EducationField.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "eq",
                "ne",
                "contains"
            }
        );

        return filterSchema;
    }
}
