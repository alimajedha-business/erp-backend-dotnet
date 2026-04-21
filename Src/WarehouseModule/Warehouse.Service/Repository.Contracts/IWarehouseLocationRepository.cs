using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IWarehouseLocationRepository : IRepository<WarehouseLocation>
{
    Task<List<WarehouseLocation>> FilterByQ(
        WarehouseLocationParameters requestParameters
    );

    IQueryable<WarehouseLocation> GetFiltered(
        Guid warehouseId,
        RequestAdvancedFilters requestAdvancedFilters
    );

    Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    );
}
