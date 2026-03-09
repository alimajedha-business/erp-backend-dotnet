using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Domain.Entities;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Service.Services;

public abstract class BaseServiceWithCompany<
    TEntity,
    TDto,
    TListDto,
    TParameters,
    TRepo,
    TResource
>(
    IAdvancedFilterBuilder filterBuilder,
    TRepo repo,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : IBaseServiceWithCompany<
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
    protected readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    protected readonly TRepo _repo = repo;
    protected readonly ICompanyService _companyService = companyService;
    protected readonly IMapper _mapper = mapper;
    protected readonly IStringLocalizer<TResource> _localizer = localizer;

    protected abstract string LocalizerKey { get; }

    protected Task EnsureCompanyAsync(Guid companyId, CancellationToken ct) =>
        _companyService.GetByIdAsync(companyId, ct);

    public virtual async Task<TDto> CreateAsync<TCreateDto>(
        Guid companyId,
        TCreateDto createDto,
        CancellationToken ct,
        Action<TEntity>? configureEntity = null
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var entity = _mapper.Map<TEntity>(createDto);
        entity.CompanyId = companyId;

        configureEntity?.Invoke(entity);

        var created = await _repo.AddAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);

        return _mapper.Map<TDto>(created);
    }

    public virtual async Task<ListResponseModel<TListDto>> GetAllAsync(
        Guid companyId,
        TParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetAllAsync(
            companyId,
            parameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ListResponseModel<TListDto>> GetAllAsync(
        Guid companyId,
        TParameters parameters,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetAllAsync(
            companyId,
            parameters,
            include,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ListResponseModel<TListDto>> GetByConditionAsync(
        Guid companyId,
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetByConditionAsync(
            companyId,
            parameters,
            expression,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ListResponseModel<TListDto>> GetByConditionAsync(
        Guid companyId,
        TParameters parameters,
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var advancedFilters = _filterBuilder.Build<TEntity>(filterNodeDto);

        var listQueryResult = await _repo.GetByConditionAsync(
            companyId,
            parameters,
            expression,
            include,
            ct,
            advancedFilters
        );

        return new ListResponseModel<TListDto>(
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<TDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var entity = await GetByIdOrThrowAsync(companyId, id, ct);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var entity = await GetByIdOrThrowAsync(companyId, id, include, ct);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> PatchAsync<TPatchDto>(
        Guid companyId,
        Guid id,
        JsonPatchDocument<TPatchDto> patchDocument,
        CancellationToken ct
    )
        where TPatchDto : class
    {
        await EnsureCompanyAsync(companyId, ct);

        var entity = await GetByIdOrThrowAsync(
            companyId,
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
        Guid companyId,
        Guid id,
        TUpdateDto updateDto,
        CancellationToken ct
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            ct,
            trackChanges: true
        );

        _mapper.Map(updateDto, entity);

        await _repo.SaveChangesAsync(ct);

        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var entity = await GetByIdOrThrowAsync(companyId, id, ct);
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
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var entity = await _repo.GetByIdAsync(companyId, id, ct, trackChanges);
        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }

    protected virtual async Task<TEntity> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var entity = await _repo.GetByIdAsync(companyId, id, include, ct, trackChanges);
        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }
}

public abstract class BaseServiceWithCompany<
    TEntity,
    TDto,
    TParameters,
    TRepo,
    TResource
>(
    IAdvancedFilterBuilder filterBuilder,
    TRepo repo,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : BaseServiceWithCompany<
        TEntity,
        TDto,
        TDto,
        TParameters,
        TRepo,
        TResource
    >(
        filterBuilder,
        repo,
        companyService,
        mapper,
        localizer
    ),
    IBaseServiceWithCompany<
        TEntity,
        TDto,
        TParameters,
        TRepo,
        TResource
    >
    where TEntity : BaseEntityWithCompany
    where TRepo : IRepositoryWithCompany<TEntity>
    where TParameters : RequestParameters
{ }