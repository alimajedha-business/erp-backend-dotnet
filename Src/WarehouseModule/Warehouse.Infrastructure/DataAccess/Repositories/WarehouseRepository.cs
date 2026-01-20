using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseRepository :
    Repository<Domain.Entities.Warehouse>,
    IWarehouseRepository
{
    public WarehouseRepository(MainDbContext context) : base(context) { }
}
