using System.Linq.Expressions;

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
        var categoryLevel = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            ct
        );

        return _mapper.Map<CategoryLevelConstraintDto>(categoryLevel);
    }

    public async Task<ListResponseModel<CategoryLevelConstraintDto>> GetFilteredAsync(
        Guid companyId,
        CategoryLevelConstraintParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<CategoryLevelConstraint>(filterNodeDto);
        var query = _constraintRepository.GetFiltered(companyId, advancedFilters);
        var res = await _constraintRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategoryLevelConstraintDto>(
            results: _mapper.Map<IReadOnlyList<CategoryLevelConstraintDto>>(res.items),
            totalCount: res.count,
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
        var categoryLevel = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchCategoryLevelConstraintDto>(categoryLevel);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, categoryLevel);

        await _constraintRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryLevelConstraintDto>(categoryLevel);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _constraintRepository.Remove(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );
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

    private async Task<CategoryLevelConstraint> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<CategoryLevelConstraint, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _constraintRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}


