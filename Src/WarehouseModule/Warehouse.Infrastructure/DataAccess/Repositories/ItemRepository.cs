using System.Linq.Dynamic.Core;

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
    public async Task<Item?> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => e.CompanyId == companyId)
            .Where(e => e.CategoryId == categoryId)
            .Where(e => e.Id == id)
            .Include(e => e.ItemType)
            .Include(e => e.Category)
            .Include(e => e.PrimaryUnitOfMeasurement)
            .SingleOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<ListQueryResult<Item>> GetCategoryAllAsync(
        Guid companyId,
        Guid categoryId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
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
                .ToListAsync();

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
                .SingleAsync();

            if (isCurrentLeaf)
                leafIds.Add(current);
        }

        leafIds = [.. leafIds.Distinct()];

        IQueryable<Item> query = _context
            .Set<Item>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Where(i => leafIds.Contains(i.CategoryId))
            .Include(e => e.ItemType)
            .Include(e => e.Category)
            .Include(e => e.PrimaryUnitOfMeasurement)
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<Item>(items, totalCount);
    }
}
