using System.Linq.Expressions;

using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Domain.Entities;

namespace NGErp.General.Service.Repository.Contracts;

public interface IRepositoryWithCompany<T> 
    : IRepository<T> where T : BaseEntityWithCompany
{
    Task<T?> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    );

    Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    );

    Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null,
        ISpecification<T>? spec = null,
        CancellationToken ct = default
    );

    IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate
    );
}
