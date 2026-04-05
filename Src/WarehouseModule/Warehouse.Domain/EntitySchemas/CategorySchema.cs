using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class CategorySchema : IFilterSchema<Category>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["parentCategoryId"] = new FilterFieldInfo(
            PropertyName: nameof(Category.ParentCategoryId),
            PropertyType: typeof(Guid?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(Category.Code),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(Category.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        filterSchema.Fields["hasNextLevel"] = new FilterFieldInfo(
            PropertyName: nameof(Category.HasNextLevel),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le", "in", "notin" }
        );

        return filterSchema;
    }
}
