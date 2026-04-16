using NGErp.Base.Service.Repository.Contracts;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseLocationRepository : IRepository<WarehouseLocation>
{
    Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    );
}
