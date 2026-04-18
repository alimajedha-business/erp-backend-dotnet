using System.Linq.Expressions;

using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Domain.Entities;

namespace NGErp.General.Service.Repository.Contracts;

public interface IRepositoryWithCompany<T> 
    : IRepository<T> where T : BaseEntityWithCompany
{
    IQueryable<T> FilterByQ(
        Guid companyId,
        RequestParameters requestParameters
    );

    IQueryable<T> GetFiltered(
        Guid companyId,
        RequestAdvancedFilters requestAdvancedFilters
    );

    IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate
    );
}
