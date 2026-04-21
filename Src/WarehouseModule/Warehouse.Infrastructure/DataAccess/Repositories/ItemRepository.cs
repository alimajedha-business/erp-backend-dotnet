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
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return await query
            .Include(i => i.ItemType)
            .Include(i => i.Category)
            .Include(i => i.PrimaryUnitOfMeasurement)
            .Include(i => i.ItemAttributes).ThenInclude(i => i.Attribute)
            .Include(i => i.ItemUnitOfMeasurements).ThenInclude(i => i.UnitOfMeasurement)
            .Include(i => i.ItemWarehouses).ThenInclude(i => i.Warehouse)
            .Include(i => i.ItemWarehouses).ThenInclude(i => i.ItemWarehouseLocations).ThenInclude(i => i.WarehouseLocation)
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
        var leafIds = new List<Guid>();
        var queue = new Queue<Guid>();
        queue.Enqueue(categoryId);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            var children = await _context.Categories
                .Where(c => c.ParentCategoryId == current)
                .Select(c => new { c.Id, c.HasNextLevel })
                .ToListAsync(cancellationToken: ct);

            foreach (var ch in children)
            {
                if (!ch.HasNextLevel)
                    leafIds.Add(ch.Id);
                else
                    queue.Enqueue(ch.Id);
            }

            var isCurrentLeaf = await _context.Categories
                .Where(c => c.Id == current)
                .Select(c => !c.HasNextLevel)
                .SingleAsync(cancellationToken: ct);

            if (isCurrentLeaf)
                leafIds.Add(current);
        }

        leafIds = [.. leafIds.Distinct()];

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
