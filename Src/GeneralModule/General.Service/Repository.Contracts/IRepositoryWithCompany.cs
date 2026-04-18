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
        CancellationToken ct,
        bool trackChanges = false
    );

    Task<T?> GetByIdAsync(
        Guid companyId,
        Guid id,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        bool trackChanges = false
    );

    IQueryable<T> GetAll(
        Guid companyId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        CancellationToken ct
    );

    Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,   
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetAllAsync(
        Guid companyId,
        RequestParameters requestParameters,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetByConditionAsync(
        Guid companyId,
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    Task<ListQueryResult<T>> GetByConditionAsync(
        Guid companyId,
        RequestParameters requestParameters,
        Expression<Func<T, bool>> conditionExpression,
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

    IQueryable<T> Find(
        Guid companyId,
        Expression<Func<T, bool>> predicate,
        IQueryable<T>? baseQuery = null
    );

    Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    );
}
