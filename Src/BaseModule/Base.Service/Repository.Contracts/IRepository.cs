using System.Linq.Expressions;

using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Repository.Contracts;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    );

    Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    );

    Task<ListQueryResult<T>> GetAllAsync(
        RequestParameters requestParameters,
        RequestAdvancedFilters requestAdvancedFilters,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    );

    IQueryable<T> Find(Expression<Func<T, bool>> predicate);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity, CancellationToken ct);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void UpdateRange(IEnumerable<T> entities);

    void Remove(Guid id, CancellationToken ct);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task<int> SaveChangesAsync(CancellationToken ct);
}
