using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ItemAttributeValueRepository :
    Repository<ItemAttributeValue>,
    IItemAttributeValueRepository
{
    public ItemAttributeValueRepository(MainDbContext context) : base(context) { }
}
