using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class UnitOfMeasurementRepository :
    Repository<UnitOfMeasurement>,
    IUnitOfMeasurementRepository
{
    public UnitOfMeasurementRepository(MainDbContext context) : base(context) { }
}
