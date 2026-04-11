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
    public async Task<ListQueryResult<WarehouseLocation>> GetWarehouseAllAsync(
        Guid warehouseId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(e => e.WarehouseId == warehouseId);

        var totalCount = await query.CountAsync(ct);
        var locations = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<WarehouseLocation>(locations, totalCount);
    }

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
            .SingleOrDefaultAsync(ct);

        if (location?.WarehouseId != warehouseId)
        {
            throw new Exception("WarehouseLocation.Warehouse.NotMatch");
        }

        return location;
    }
}
