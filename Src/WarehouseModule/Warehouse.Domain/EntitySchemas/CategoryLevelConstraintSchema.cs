using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class CategoryLevelConstraintSchema :
    IFilterSchema<CategoryLevelConstraint>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        return filterSchema;
    }
}
