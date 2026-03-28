using System.Linq.Expressions;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Domain.Entities;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Service.Services;

public interface IBaseServiceWithCompany<
    TEntity,
    TDto,
    TListDto,
    TParameters,
    TRepo,
    TResource
>
    where TEntity : BaseEntityWithCompany
    where TRepo : IRepositoryWithCompany<TEntity>
    where TParameters : RequestParameters
{
    Task<TDto> CreateAsync<TCreateDto>(
        Guid companyId,
        TCreateDto createDto,
        CancellationToken ct,
        Action<TEntity>? configureEntity = null
    );

    Task<ListResponseModel<TListDto>> GetAllAsync(
        Guid companyId,
        TParameters parameters,
        CancellationToken ct
    );

    Task<ListResponseModel<TListDto>> GetAllAsync(
        Guid companyId,
        TParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ListResponseModel<TListDto>> GetAllAsync(
        Guid companyId,
        TParameters parameters,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ListResponseModel<TListDto>> GetByConditionAsync(
        Guid companyId,
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ListResponseModel<TListDto>> GetByConditionAsync(
        Guid companyId,
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<TDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<TDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct
    );

    Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    );

    Task<TDto> PatchAsync<TPatchDto>(
        Guid companyId,
        Guid id,
        JsonPatchDocument<TPatchDto> patchDocument,
        CancellationToken ct
    )
        where TPatchDto : class;

    Task<TDto> UpdateAsync<TUpdateDto>(
        Guid companyId,
        Guid id,
        TUpdateDto updateDto,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}

public interface IBaseServiceWithCompany<
    TEntity,
    TDto,
    TParameters,
    TRepo,
    TResource
> : IBaseServiceWithCompany<
        TEntity,
        TDto,
        TDto,
        TParameters,
        TRepo,
        TResource>
    where TEntity : BaseEntityWithCompany
    where TRepo : IRepositoryWithCompany<TEntity>
    where TParameters : RequestParameters
{ }