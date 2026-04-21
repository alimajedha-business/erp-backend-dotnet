using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IUnitOfMeasurementRepository : IRepository<UnitOfMeasurement>
{
    Task<int> GetNextCodeAsync(CancellationToken ct);
}
