using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseLocationRepository : IRepository<WarehouseLocation>
{
    Task<WarehouseLocation?> GetByIdAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    );

    Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    );

    Task<ListQueryResult<WarehouseLocation>> GetWarehouseLocationsAsync(
        Guid warehouseId,
        RequestParameters requestParameters,
        CancellationToken ct
    );

    Task<ListQueryResult<WarehouseLocation>> GetWarehouseLocationsAsync(
        Guid warehouseId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
}
