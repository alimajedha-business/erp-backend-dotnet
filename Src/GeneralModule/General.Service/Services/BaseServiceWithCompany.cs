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
    IRepo,
    TResource
>(
    IAdvancedFilterBuilder filterBuilder,
    IRepo repo,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : IBaseServiceWithCompany<
        TEntity,
        TDto,
        TListDto,
        TParameters,
        IRepo,
        TResource
    >
    where TEntity : BaseEntityWithCompany
    where IRepo : IRepositoryWithCompany<TEntity>
    where TParameters : RequestParameters
{
    protected readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    protected readonly IRepo _repo = repo;
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

        await TrySaveMutationAsync(ct);
        return _mapper.Map<TDto>(created);
    }

    public virtual async Task<ListResponseModel<TListDto>> GetAllAsync(
        Guid companyId,
        TParameters parameters,
        CancellationToken ct
    )
    {
        await EnsureCompanyAsync(companyId, ct);

        var listQueryResult = await _repo.GetAllAsync(
            companyId,
            parameters,
            ct
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

    public virtual async Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return await _repo.GetNextCode(companyId, ct);
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

        await TrySaveMutationAsync(ct);
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

    protected virtual async Task TrySaveMutationAsync(CancellationToken ct)
    {
        try
        {
            await _repo.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {

            // 2601, 2627: duplicate key / unique index
            if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
            {
                throw new DuplicateInsertException(_localizer[LocalizerKey].Value);
            }
            // 547: FK or check constraint
            else if (sqlEx.Number == 547)
            {
                var match = RegexHelpers.SqlConstraintRegex().Match(sqlEx.Message);
                string constraintName = match.Groups["name"].Value;

                throw new CheckConstraintException(
                    _localizer[LocalizerKey].Value,
                    _localizer[constraintName].Value
                );
            }
            else
            {
                // Handle other SQL errors
                throw new Exception("Xxx");
            }
        }
        catch (Exception)
        {
            // Handle other general exceptions
            throw new Exception("Yyy");
        }
    }
}

public abstract class BaseServiceWithCompany<
    TEntity,
    TDto,
    TParameters,
    IRepo,
    TResource
>(
    IAdvancedFilterBuilder filterBuilder,
    IRepo repo,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : BaseServiceWithCompany<
        TEntity,
        TDto,
        TDto,
        TParameters,
        IRepo,
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
        IRepo,
        TResource
    >
    where TEntity : BaseEntityWithCompany
    where IRepo : IRepositoryWithCompany<TEntity>
    where TParameters : RequestParameters
{ }