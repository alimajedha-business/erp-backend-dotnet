using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class UnitOfMeasurementConversionRepository :
    Repository<UnitOfMeasurementConversion>,
    IUnitOfMeasurementConversionRepository
{
    public UnitOfMeasurementConversionRepository(MainDbContext context) : base(context) { }
}
