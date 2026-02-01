using System.Linq.Expressions;

using NGErp.Base.Service.Repository.Contract;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Domain.Entities;

namespace NGErp.General.Infrastructure.DataAccess.Repositories;

public interface IRepositoryWithCompany<T> 
    : IRepository<T> where T : BaseEntityWithCompany
{
    Task<T?> GetByIdAsync(Guid companyId, Guid id);
    IQueryable<T> GetList(
        Guid companyId,
        RequestAdvancedFilters? requestAdvancedFilters = null,
        IQueryable<T>? baseQuery = null
    );
    IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate,
        IQueryable<T>? baseQuery = null
    );
}
