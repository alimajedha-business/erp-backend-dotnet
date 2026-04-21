using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseLocationRepository(MainDbContext context) :
    Repository<WarehouseLocation>(context),
    IWarehouseLocationRepository
{
    public override Task<WarehouseLocation?> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return query
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<List<WarehouseLocation>> FilterByQ(
        WarehouseLocationParameters requestParameters
    )
    {
        var warehouseId = requestParameters.WarehouseId;
        var locations = await _dbSet
            .Where(e => e.WarehouseId == warehouseId)
            .Select(s => new
            {
                s.Id,
                s.Code,
                s.Title,
                s.ParentLocationId
            })
            .ToListAsync();

        var byId = locations.ToDictionary(
            x => x.Id,
            x => new Node(x.Id, x.Code, x.Title, x.ParentLocationId));

        var cache = new Dictionary<Guid, string>();

        string BuildPath(Guid id, HashSet<Guid>? visited = null)
        {
            visited ??= [];

            if (!visited.Add(id))
                throw new InvalidOperationException("Cycle detected.");

            if (cache.TryGetValue(id, out var cached))
                return cached;

            var node = byId[id];

            var path = node.ParentLocationId is null
                ? node.Title
                : $"{BuildPath(node.ParentLocationId.Value, visited)}-{node.Title}";

            cache[id] = path;
            visited.Remove(id);
            return path;
        }

        return [.. locations
            .Select(s => new WarehouseLocation
            {
                Id = s.Id,
                Code = s.Code,
                Title = BuildPath(s.Id)
            })
            .OrderBy(x => x.Code)];
    }

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

    private sealed record Node(
        Guid Id,
        int Code,
        string Title,
        Guid? ParentLocationId
    );
}
