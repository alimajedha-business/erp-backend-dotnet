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

        return query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
    }

    public virtual IQueryable<T> FilterByQ(
        RequestParameters requestParameters
    )
    {
        return _context
            .Set<T>()
            .AsNoTracking()
            .Filter(requestParameters);
    }

    public virtual IQueryable<T> GetFiltered(
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return _context
            .Set<T>()
            .AsNoTracking()
            .Filter(requestAdvancedFilters);
    }

    public virtual IQueryable<T> Find(
        Expression<Func<T, bool>> predicate
    )
    {
        return _dbSet.Where(predicate);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate
    )
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate
    )
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual async Task<T> AddAsync(
        T entity,
        CancellationToken ct
    )
    {
        await _dbSet.AddAsync(entity, ct);
        return entity;
    }

    public virtual async Task AddRangeAsync(
        IEnumerable<T> entities
    )
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(
        IEnumerable<T> entities
    )
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Remove(Guid id, CancellationToken ct)
    {
        _dbSet
            .Where(e => EF.Property<Guid>(e, "Id") == id)
            .ExecuteDeleteAsync(ct);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(
        IEnumerable<T> entities
    )
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<ListQueryResult<T>> GetResponseListAsync(
        IQueryable<T> query,
        RequestParameters requestParameters,
        CancellationToken ct
    )
    {
        return await query.ToResponseListAsync(requestParameters, ct);
    }

    public virtual async Task<int> SaveChangesAsync(
        CancellationToken ct
    )
    {
        return await _context.SaveChangesAsync(ct);
    }
}

