using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

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
        IRepo,
        TResource
    >(
    IAdvancedFilterBuilder filterBuilder,
    IRepo repo,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : IBaseService<
        TEntity,
        TDto,
        TListDto,
        TParameters,
        IRepo,
        TResource
    >
    where TEntity : BaseEntity
    where IRepo : IRepository<TEntity>
    where TParameters : RequestParameters
{
    protected readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    protected readonly IRepo _repo = repo;
    protected readonly IMapper _mapper = mapper;
    protected readonly IStringLocalizer<TResource> _localizer = localizer;

    protected abstract string LocalizerKey { get; }

    public virtual async Task<TDto> CreateAsync<TCreateDto>(
        TCreateDto createDto,
        CancellationToken ct,
        Action<TEntity>? configureEntity = null
    )
    {
        var entity = _mapper.Map<TEntity>(createDto);

        configureEntity?.Invoke(entity);

        try
        {
            var created = await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<TDto>(created);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            HandleSqlExceptionOnCreate(sqlEx);
            throw new Exception("Xxx");
        }
        catch (Exception)
        {
            // Handle other general exceptions
            throw new Exception("Yyy");
        }
    }

    public virtual async Task<ListResponseModel<TListDto>> GetAllAsync(
       TParameters parameters,
       CancellationToken ct
    )
    {
        var listQueryResult = await _repo.GetAllAsync(
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
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
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
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
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
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
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
            results: _mapper.Map<IReadOnlyList<TListDto>>(listQueryResult.items),
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
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            HandleSqlExceptionOnCreate(sqlEx);
        }
        catch (Exception)
        {
            // Handle other general exceptions
            throw new Exception("Yyy");
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

    protected virtual void HandleSqlExceptionOnCreate(SqlException sqlEx)
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
}

public abstract class BaseService<
    TEntity,
    TDto,
    TParameters,
    IRepo,
    TResource
>(
    IAdvancedFilterBuilder filterBuilder,
    IRepo repo,
    IMapper mapper,
    IStringLocalizer<TResource> localizer
) : BaseService<
        TEntity,
        TDto,
        TDto,
        TParameters,
        IRepo,
        TResource
    >(
        filterBuilder,
        repo,
        mapper,
        localizer
    ),
    IBaseService<
        TEntity,
        TDto,
        TParameters,
        IRepo,
        TResource
    >
    where TEntity : BaseEntity
    where IRepo : IRepository<TEntity>
    where TParameters : RequestParameters
{ }