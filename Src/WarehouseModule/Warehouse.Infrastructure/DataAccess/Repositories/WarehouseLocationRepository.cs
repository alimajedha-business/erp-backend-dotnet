using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseLocationRepository :
    Repository<WarehouseLocation>,
    IRepository<WarehouseLocation>
{
    public WarehouseLocationRepository(MainDbContext context) : base(context) { }
}
