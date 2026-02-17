using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryAttributeRuleRepository(MainDbContext context) :
    Repository<CategoryAttributeRule>(context),
    ICategoryAttributeRuleRepository
{
    public override Task<CategoryAttributeRule?> GetByIdAsync(
        Guid id,
        CancellationToken ct,
        bool trackChanges
    )
    {
        var query = trackChanges ? _context.Set<CategoryAttributeRule>()
                                 : _context.Set<CategoryAttributeRule>().AsNoTracking();

        return query
            .Where(e => e.Id == id)
            .Include(e => e.Category)
            .Include(e => e.Attribute)
            .FirstOrDefaultAsync(ct);
    }

    public override async Task<ListQueryResult<CategoryAttributeRule>> GetByConditionAsync(
        RequestParameters requestParameters,
        Expression<Func<CategoryAttributeRule, bool>> conditionExpression,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<CategoryAttributeRule> query = _context
            .Set<CategoryAttributeRule>()
            .AsNoTracking()
            .Where(conditionExpression)
            .Include(e => e.Category)
            .Include(e => e.Attribute)
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<CategoryAttributeRule>(items, totalCount);
    }
}
