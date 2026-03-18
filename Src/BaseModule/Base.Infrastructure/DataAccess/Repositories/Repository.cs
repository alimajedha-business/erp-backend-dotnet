using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Infrastructure.DataAccess.Repositories;

public class Repository<T>(MainDbContext context) : IRepository<T> where T : class
{
    protected readonly MainDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
    }

    public virtual Task<T?> GetByIdAsync(
        Guid id,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        query = include(query);

        return query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
    }

    public virtual IQueryable<T> GetAll(
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        return _context
            .Set<T>()
            .AsNoTracking()
            .Filter(requestAdvancedFilters);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        CancellationToken ct
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Filter(requestParameters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Filter(requestAdvancedFilters);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
            .Filter(requestAdvancedFilters);

        query = include(query);

        var totalCount = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    public virtual async Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
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
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        IQueryable<T> query = _context
            .Set<T>()
            .AsNoTracking()
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
        Expression<Func<T, bool>> predicate,
        IQueryable<T>? baseQuery = null
    )
    {
        return (baseQuery ?? _dbSet).Where(predicate);
    }

    public virtual async Task<int> GetNextCode(CancellationToken ct)
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
            .Select(e => EF.Property<int>(e, "Code"))
            .OrderByDescending(e => e)
            .FirstOrDefaultAsync(ct);

        return code + 1;
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity, ct);
        return entity;
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }
}

