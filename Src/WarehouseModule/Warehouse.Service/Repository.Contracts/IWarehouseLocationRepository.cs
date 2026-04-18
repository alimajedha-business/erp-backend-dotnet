using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseLocationRepository : IRepository<WarehouseLocation>
{
    IQueryable<WarehouseLocation> GetFiltered(
        Guid warehouseId,
        RequestAdvancedFilters requestAdvancedFilters
    );

    Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    );
}
