using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class MeasurementDimensionRepository(MainDbContext context) :
    Repository<MeasurementDimension>(context),
    IMeasurementDimensionRepository
{ }
