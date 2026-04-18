using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseLocationRepository(MainDbContext context) :
    Repository<WarehouseLocation>(context),
    IWarehouseLocationRepository
{
    public IQueryable<WarehouseLocation> GetFiltered(
        Guid warehouseId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        var query = base.GetFiltered(requestAdvancedFilters);
        return query
            .Where(e => e.WarehouseId == warehouseId)
            .Include(i => i.Warehouse);
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
}
