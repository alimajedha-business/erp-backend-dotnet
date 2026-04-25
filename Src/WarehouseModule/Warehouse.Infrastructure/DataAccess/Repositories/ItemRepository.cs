using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class ItemRepository(MainDbContext context) :
    RepositoryWithCompany<Item>(context),
    IItemRepository
{
    public override async Task<Item?> SingleOrDefaultAsync(
        Expression<Func<Item, bool>> predicate,
        bool trackChanges = true,
        CancellationToken ct = default
    )
    {
        var query = (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .AsSplitQuery();

        return await query
            .Include(i => i.ItemType)
            .Include(i => i.Category)
            .Include(i => i.PrimaryUnitOfMeasurement)
            .Include(i => i.ItemAttributes)
                .ThenInclude(i => i.Attribute)
            .Include(i => i.ItemUnitOfMeasurements)
                .ThenInclude(i => i.UnitOfMeasurement)
            .Include(i => i.ItemWarehouses)
                .ThenInclude(i => i.Warehouse)
            .Include(i => i.ItemWarehouses)
                .ThenInclude(i => i.ItemWarehouseLocations)
                .ThenInclude(i => i.WarehouseLocation)
            .Include(i => i.ItemUnitOfMeasurementConversions)
            .SingleOrDefaultAsync(predicate, ct);
    }

    public async Task<ListQueryResult<Item>> GetCategoryAllAsync(
        Guid companyId,
        Guid categoryId,
        RequestParameters requestParameters,
        RequestAdvancedFilters? requestAdvancedFilters,
        CancellationToken ct
    )
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId)
            .Select(c => new
            {
                c.Id,
                c.ParentCategoryId,
                c.HasNextLevel
            })
            .ToListAsync(ct);

        var byId = categories.ToDictionary(c => c.Id);
        if (!byId.TryGetValue(categoryId, out _))
            return new ListQueryResult<Item>([], 0);

        var childrenByParent = categories
            .Where(c => c.ParentCategoryId.HasValue)
            .GroupBy(c => c.ParentCategoryId!.Value)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

        var leafIds = new HashSet<Guid>();
        var visited = new HashSet<Guid>();
        var queue = new Queue<Guid>();
        queue.Enqueue(categoryId);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!visited.Add(current))
                continue;

            var node = byId[current];
            if (!node.HasNextLevel || !childrenByParent.TryGetValue(current, out var children))
            {
                leafIds.Add(current);
                continue;
            }

            foreach (var childId in children)
            {
                queue.Enqueue(childId);
            }
        }

        if (leafIds.Count == 0)
            return new ListQueryResult<Item>([], 0);

        IQueryable<Item> query = _context
            .Set<Item>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Where(e => leafIds.Contains(e.CategoryId))
            .Include(i => i.ItemType)
            .Include(i => i.Category)
            .Include(i => i.PrimaryUnitOfMeasurement)
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<Item>(items, totalCount);
    }
}
