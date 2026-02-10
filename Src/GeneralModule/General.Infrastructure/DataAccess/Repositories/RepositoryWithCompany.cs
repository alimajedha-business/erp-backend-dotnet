using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Domain.Entities;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Infrastructure.DataAccess.Repositories;

public class RepositoryWithCompany<T>(MainDbContext context) :
    Repository<T>(context),
    IRepositoryWithCompany<T> where T : BaseEntityWithCompany
{
    public virtual async Task<T?> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        return await query.FirstOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            ct);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate,
        IQueryable<T>? baseQuery = null
    )
    {
        return (baseQuery ?? _dbSet)
            .Where(e => e.CompanyId == companyId)
            .Where(predicate);
    }
}