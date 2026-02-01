using System.Linq.Expressions;

using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Infrastructure.DataAccess.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> GetList(
        RequestAdvancedFilters? requestAdvancedFilters = null,
        IQueryable<T>? baseQuery = null
    );
    IQueryable<T> Find(
        Expression<Func<T, bool>> predicate,
        IQueryable<T>? baseQuery = null
    );
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
