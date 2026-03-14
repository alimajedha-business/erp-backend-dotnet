using System.Linq.Expressions;
using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Domain.Entities;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Services;

public interface IBaseService<
    TEntity,
    TDto,
    TListDto,
    TParameters,
    TRepo,
    TResource
>
    where TEntity : BaseEntity
    where TRepo : IRepository<TEntity>
    where TParameters : RequestParameters
{
    Task<TDto> CreateAsync<TCreateDto>(
        TCreateDto createDto,
        CancellationToken ct,
        Action<TEntity>? configureEntity = null
    );

    Task<ListResponseModel<TListDto>> GetAllAsync(
        TParameters parameters,
        CancellationToken ct
    );

    Task<ListResponseModel<TListDto>> GetAllAsync(
        TParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ListResponseModel<TListDto>> GetAllAsync(
        TParameters parameters,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ListResponseModel<TListDto>> GetByConditionAsync(
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ListResponseModel<TListDto>> GetByConditionAsync(
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<TDto> GetByIdAsync(
        Guid id,
        CancellationToken ct
    );

    Task<TDto> GetByIdAsync(
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct
    );

    Task<int> GetNextCode(CancellationToken ct);

    Task<TDto> PatchAsync<TPatchDto>(
        Guid id,
        JsonPatchDocument<TPatchDto> patchDocument,
        CancellationToken ct
    )
        where TPatchDto : class;

    Task<TDto> UpdateAsync<TUpdateDto>(
        Guid id,
        TUpdateDto updateDto,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}

public interface IBaseService<
    TEntity,
    TDto,
    TParameters,
    TRepo,
    TResource
> : IBaseService<
        TEntity,
        TDto,
        TDto,
        TParameters,
        TRepo,
        TResource>
    where TEntity : BaseEntity
    where TRepo : IRepository<TEntity>
    where TParameters : RequestParameters
{ }