using System.Linq.Expressions;

using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Repository.Contracts;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    );

    Task<T?> GetByIdAsync(
        Guid id,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        bool trackChanges = false
    );

    IQueryable<T> GetAll(
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        CancellationToken ct
    );

    Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetByConditionAsync(
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetByConditionAsync(
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    IQueryable<T> Find(Expression<Func<T, bool>> predicate);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity, CancellationToken ct);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void UpdateRange(IEnumerable<T> entities);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task<int> SaveChangesAsync(CancellationToken ct);
}
