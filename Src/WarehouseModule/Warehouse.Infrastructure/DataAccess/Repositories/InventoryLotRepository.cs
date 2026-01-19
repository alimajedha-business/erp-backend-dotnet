using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class InventoryLotRepository :
    Repository<InventoryLot>,
    IInventoryLotRepository
{
    public InventoryLotRepository(MainDbContext context) : base(context) { }
}
