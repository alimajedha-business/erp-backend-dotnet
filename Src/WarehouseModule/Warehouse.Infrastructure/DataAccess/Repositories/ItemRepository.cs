using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ItemRepository :
    Repository<Item>,
    IItemRepository
{
    public ItemRepository(MainDbContext context) : base(context) { }
}
