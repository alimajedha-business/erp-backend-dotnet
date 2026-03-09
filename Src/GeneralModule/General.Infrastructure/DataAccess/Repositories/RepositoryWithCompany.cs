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
    public virtual Task<T?> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return query.FirstOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            ct);
    }

    public virtual Task<T?> GetByIdAsync(
        Guid companyId,
        Guid id,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        query = include(query);

        return query.FirstOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == id,
            ct);
    }

    public virtual IQueryable<T> GetAll(
        Guid companyId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        return _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
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

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);

        query = include(query);

        var totalCount = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);

        query = include(query);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetByConditionAsync(
        Guid companyId,
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Where(conditionExpression)
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetByConditionAsync(
        Guid companyId,
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Where(conditionExpression)
            .Filter(requestAdvancedFilters);

        query = include(query);

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

    public virtual async Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        var codeField = typeof(T).GetProperty("Code");
        if (codeField == null)
        {
            throw new Exception("Property named Code does not exist.");
        }

        if (codeField.PropertyType != typeof(int))
        {
            throw new Exception("Type of property Code is not integer.");
        }

        var code = await _dbSet
            .Where(e => e.CompanyId == companyId)
            .Select(e => EF.Property<int>(e, "Code"))
            .OrderByDescending(e => e)
            .FirstOrDefaultAsync(ct);

        return code + 1;
    }
}