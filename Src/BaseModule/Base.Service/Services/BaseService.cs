using System.ComponentModel.Design;
using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Entities;
using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Services;

public abstract class BaseService<
        TEntity,
        TDto,
        TListDto,
        TParameters,
        TRepo,
        TResource
    >(
    IAdvancedFilterBuilder filterBuilder,
    TRepo repo,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
)
    where TEntity : BaseEntity
    where TRepo : IRepository<TEntity>
    where TParameters : RequestParameters
{
    protected readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    protected readonly TRepo _repo = repo;
    protected readonly IMapper _mapper = mapper;
    protected readonly IStringLocalizer<TResource> _localizer = localizer;

    protected abstract string LocalizerKey { get; }

    public virtual async Task<TDto> CreateAsync<TCreateDto>(
        TCreateDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<TEntity>(createDto);

        var created = await _repo.AddAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);

        return _mapper.Map<TDto>(created);
    }

    public virtual async Task<ListResponseModel<TListDto>> GetAllAsync(
       TParameters parameters,
       CancellationToken ct,
       FilterNodeDto? filterNodeDto = null
    )
    {
        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetAllAsync(
            parameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            items: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ListResponseModel<TListDto>> GetAllAsync(
       TParameters parameters,
       Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
       CancellationToken ct,
       FilterNodeDto? filterNodeDto = null
    )
    {
        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetAllAsync(
            parameters,
            include,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            items: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ListResponseModel<TListDto>> GetByConditionAsync(
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetByConditionAsync(
            parameters,
            expression,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            items: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ListResponseModel<TListDto>> GetByConditionAsync(
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetByConditionAsync(
            parameters,
            expression,
            include,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            items: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<TDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await GetByIdOrThrowAsync(id, ct);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetByIdAsync(
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, include, ct);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> PatchAsync<TPatchDto>(
        Guid id,
        JsonPatchDocument<TPatchDto> patchDocument,
        CancellationToken ct
    )
        where TPatchDto : class
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            ct,
            trackChanges: true
        );

        var patchDto = _mapper.Map<TPatchDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
            throw new InvalidPatchDocumentException(errors);

        _mapper.Map(patchDto, entity);

        try
        {
            await _repo.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateBadRequestException(ex.Message);
        }

        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> UpdateAsync<TUpdateDto>(
        Guid id,
        TUpdateDto updateDto,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            ct,
            trackChanges: true
        );

        _mapper.Map(updateDto, entity);

        await _repo.SaveChangesAsync(ct);

        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, ct);
        _repo.Remove(entity);

        try
        {
            await _repo.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer[LocalizerKey].Value);
        }

        return;
    }

    protected virtual async Task<TEntity> GetByIdOrThrowAsync(
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var entity = await _repo.GetByIdAsync(id, ct, trackChanges);
        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }

    protected virtual async Task<TEntity> GetByIdOrThrowAsync(
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var entity = await _repo.GetByIdAsync(id, include, ct, trackChanges);
        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }
}

public abstract class BaseService<
    TEntity,
    TDto,
    TParameters,
    TRepo,
    TResource
>(
    IAdvancedFilterBuilder filterBuilder,
    TRepo repo,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : BaseService<
        TEntity,
        TDto,
        TDto,
        TParameters,
        TRepo,
        TResource
    >(
        filterBuilder,
        repo,
        mapper,
        localizer
    )
    where TEntity : BaseEntity
    where TRepo : IRepository<TEntity>
    where TParameters : RequestParameters
{ }