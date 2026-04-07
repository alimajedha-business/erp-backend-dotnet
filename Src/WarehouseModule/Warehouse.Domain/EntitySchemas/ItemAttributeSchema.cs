using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class ItemAttributeSchema : IFilterSchema<ItemAttribute>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();
        return filterSchema;
    }
}
