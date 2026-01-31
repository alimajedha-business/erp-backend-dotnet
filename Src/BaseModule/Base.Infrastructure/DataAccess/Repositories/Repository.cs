using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Infrastructure.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MainDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MainDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetList(
            RequestAdvancedFilters? requestAdvancedFilters = null,
            IQueryable<T>? baseQuery = null
        )
        {
            IQueryable<T> query = baseQuery ?? _context.Set<T>();

            if (requestAdvancedFilters != null)
            {
                var (search, args) = requestAdvancedFilters;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = (args is { Length: > 0 })
                        ? query.Where(search, args)
                        : query.Where(search);
                }
            }

            return query;
        }

        public virtual IQueryable<T> Find(
            Expression<Func<T, bool>> predicate,
            IQueryable<T>? baseQuery = null
        )
        {
            return (baseQuery ?? _dbSet).Where(predicate);
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
}

