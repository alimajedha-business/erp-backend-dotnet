using System.Linq.Dynamic.Core;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.RequestFeatures;

using System.Linq.Expressions;

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

        public virtual IQueryable<T> GetAllAsync()
        {
            return _context.Set<T>();
        }

        public virtual IQueryable<T> GetPaginated(
            RequestParameters requestParameters,
            string? search = null,
            object[]? searchParameters = null
        )
        {
            IQueryable<T> query = _context.Set<T>();

            // 1- apply filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = (searchParameters is { Length: > 0 })
                    ? query.Where(search, searchParameters)
                    : query.Where(search);
            }

            // 2- apply sorting
            if (!string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                var orderByClause = requestParameters.OrderBy.Trim();

                // support "-Field" to mean DESC
                if (orderByClause.StartsWith('-'))
                    orderByClause = orderByClause.TrimStart('-') + " DESC";

                query = query.OrderBy(orderByClause);
            }

            // 3- apply paging
            return query
                .Skip(requestParameters.PageSize * (requestParameters.PageNumber - 1))
                .Take(requestParameters.PageSize);
        }


        public virtual IQueryable<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
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

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

