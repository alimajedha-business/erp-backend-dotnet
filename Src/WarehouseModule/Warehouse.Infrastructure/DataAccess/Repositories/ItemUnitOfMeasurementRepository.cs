using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ItemUnitOfMeasurementRepository :
    Repository<ItemUnitOfMeasurement>,
    IItemUnitOfMeasurementRepository
{
    public ItemUnitOfMeasurementRepository(MainDbContext context) : base(context) { }
}
