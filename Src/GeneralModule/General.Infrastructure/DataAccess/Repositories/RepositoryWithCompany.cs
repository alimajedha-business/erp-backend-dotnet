using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Domain.Entities;

namespace NGErp.General.Infrastructure.DataAccess.Repositories;

public class RepositoryWithCompany<T>(MainDbContext context) : 
    Repository<T>(context),
    IRepositoryWithCompany<T> where T : BaseEntityWithCompany
{
    public virtual async Task<T?> GetByIdAsync(Guid companyId, Guid id)
    {
        return await _dbSet
            .Where(e => e.CompanyId == companyId && e.Id == id)
            .SingleOrDefaultAsync();
    }

    public virtual IQueryable<T> GetAll(
        Guid companyId,
        RequestAdvancedFilters? requestAdvancedFilters = null,
        IQueryable<T>? baseQuery = null
    )
    {
        IQueryable<T> query = baseQuery ?? _context
            .Set<T>()
            .Where(e => e.CompanyId == companyId);

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
        Guid companyId,
        Expression<Func<T, bool>> predicate,
        IQueryable<T>? baseQuery = null
    )
    {
        return (baseQuery ?? _dbSet)
            .Where(e => e.CompanyId == companyId)
            .Where(predicate);
    }
}
