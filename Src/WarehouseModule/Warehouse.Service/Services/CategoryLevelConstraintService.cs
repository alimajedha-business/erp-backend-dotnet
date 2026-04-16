using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class CategoryLevelConstraintService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryLevelConstraintRepository constraintRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : ICategoryLevelConstraintService
{
    private readonly string _key = "CategoryLevelConstraint";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICategoryLevelConstraintRepository _constraintRepository = constraintRepository;

    public async Task<CategoryLevelConstraintDto> CreateAsync(
        Guid companyId,
        CreateCategoryLevelConstraintDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<CategoryLevelConstraint>(createDto);
        entity.CompanyId = companyId;

        var created = await _constraintRepository.AddAsync(entity, ct);

        await _constraintRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryLevelConstraintDto>(created);
    }

    public async Task<CategoryLevelConstraintDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<CategoryLevelConstraintDto>(entity);
    }

    public async Task<ListResponseModel<CategoryLevelConstraintDto>> GetAllAsync(
        Guid companyId,
        CategoryLevelConstraintParameters parameters,
        CancellationToken ct = default
    )
    {
        var listQueryResult = await _constraintRepository.GetAllAsync(
            companyId,
            parameters,
            spec: null,
            ct
        );

        return new ListResponseModel<CategoryLevelConstraintDto>(
            results: _mapper.Map<IReadOnlyList<CategoryLevelConstraintDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<CategoryLevelConstraintDto>> GetAllAsync(
        Guid companyId,
        CategoryLevelConstraintParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<CategoryLevelConstraint>(filterNodeDto);
        var listQueryResult = await _constraintRepository.GetAllAsync(
            parameters,
            advancedFilters,
            spec: null,
            ct
        );

        return new ListResponseModel<CategoryLevelConstraintDto>(
            results: _mapper.Map<IReadOnlyList<CategoryLevelConstraintDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<CategoryLevelConstraintDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryLevelConstraintDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchCategoryLevelConstraintDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _constraintRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryLevelConstraintDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        _constraintRepository.Remove(entity);
        await _constraintRepository.SaveChangesAsync(ct);
    }

    public async Task<CategoryLevelConstraintDto> GetByLevelNoAsync(
        Guid companyId,
        int levelNo,
        CancellationToken ct
    )
    {
        var constraint = await _constraintRepository.GetByLevelNoAsync(companyId, levelNo, ct);
        return _mapper.Map<CategoryLevelConstraintDto>(constraint);
    }

    public async Task<int> GetNextLevelAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        return await _constraintRepository.GetNextLevelAsync(companyId, ct);
    }

    private async Task<CategoryLevelConstraint> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _constraintRepository.GetByIdAsync(
            companyId,
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
