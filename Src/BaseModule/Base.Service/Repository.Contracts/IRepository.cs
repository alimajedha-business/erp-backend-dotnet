using System.Linq.Expressions;

using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Repository.Contracts;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    IQueryable<T> FilterByQ(RequestParameters requestParameters);

    IQueryable<T> GetFiltered(RequestAdvancedFilters requestAdvancedFilters);

    IQueryable<T> Find(Expression<Func<T, bool>> predicate);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity, CancellationToken ct);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void UpdateRange(IEnumerable<T> entities);

    void Remove(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct
    );

    void Remove(Guid id, CancellationToken ct);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task<ListQueryResult<T>> GetResponseListAsync(
        IQueryable<T> query,
        RequestParameters requestParameters,
        CancellationToken ct
    );

    Task<int> SaveChangesAsync(CancellationToken ct);
}
