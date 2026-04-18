using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Domain.Entities;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Infrastructure.DataAccess.Repositories;

public class RepositoryWithCompany<T>(MainDbContext context) :
    Repository<T>(context),
    IRepositoryWithCompany<T> where T : BaseEntityWithCompany
{
    public virtual IQueryable<T> FilterByQ(
        Guid companyId,
        RequestParameters requestParameters
    )
    {
        return _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestParameters);
    }

    public virtual IQueryable<T> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    )
    {
        return _dbSet
            .AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .Filter(requestAdvancedFilters);
    }

    public virtual IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate
    )
    {
        return _dbSet
            .Where(e => e.CompanyId == companyId)
            .Where(predicate);
    }
}