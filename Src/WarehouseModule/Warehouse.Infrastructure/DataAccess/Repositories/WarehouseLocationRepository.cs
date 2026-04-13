using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseLocationRepository(MainDbContext context) :
    Repository<WarehouseLocation>(context),
    IWarehouseLocationRepository
{
    public async Task<WarehouseLocation?> GetByIdAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        var location = await query
            .Where(e => e.Id == id)
            .Include(i => i.Warehouse)
            .SingleOrDefaultAsync(ct);

        if (location?.WarehouseId != warehouseId)
        {
            throw new Exception("WarehouseLocation.Warehouse.NotMatch");
        }

        return location;
    }

    public async Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    )
    {
        var maxCode = await _dbSet
            .AsNoTracking()
            .Where(e => e.WarehouseId == warehouseId)
            .MaxAsync(e => (int?)e.Code, ct);

        return (maxCode ?? 0) + 1;
    }

    public Task<ListQueryResult<WarehouseLocation>> GetWarehouseLocationsAsync(
    Guid warehouseId,
    RequestParameters requestParameters,
    CancellationToken ct)
    {
        return GetWarehouseLocationsAsync(
            warehouseId,
            q => q.Filter(requestParameters),
            ct
        );
    }

    public Task<ListQueryResult<WarehouseLocation>> GetWarehouseLocationsAsync(
        Guid warehouseId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null)
    {
        return GetWarehouseLocationsAsync(
            warehouseId,
            q => q.Filter(requestAdvancedFilters),
            ct
        );
    }

    private async Task<ListQueryResult<WarehouseLocation>> GetWarehouseLocationsAsync(
    Guid warehouseId,
    Func<IQueryable<WarehouseLocation>, IQueryable<WarehouseLocation>> applyFilter,
    CancellationToken ct)
    {
        IQueryable<WarehouseLocation> query = _dbSet
            .AsNoTracking()
            .Where(e => e.WarehouseId == warehouseId)
            .Include(i => i.Warehouse);

        query = applyFilter(query);

        var totalCount = await query.CountAsync(ct);
        var locations = await query.ToListAsync(ct);

        return new ListQueryResult<WarehouseLocation>(locations, totalCount);
    }
}
