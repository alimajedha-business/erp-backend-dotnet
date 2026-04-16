using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Domain.Entities;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Infrastructure.DataAccess.Repositories;

public class RepositoryWithCompany<T>(MainDbContext context) :
    Repository<T>(context),
    IRepositoryWithCompany<T> where T : BaseEntityWithCompany
{
    public virtual Task<T?> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();

        if (spec != null)
        {
            query = spec.Query(query);
        }

        return query.FirstOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            ct);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestParameters);

        if (spec != null)
        {
            query = spec.Query(query);
        }

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);

        if (spec != null)
        {
            query = spec.Query(query);
        }

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate
    )
    {
        return _dbSet
            .Where(e => e.CompanyId == companyId)
            .Where(predicate);
    }
}