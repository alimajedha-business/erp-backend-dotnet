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

public class CategoryAttributeRuleService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryAttributeRuleRepository attributeRuleRepository,
    ICategoryService categoryService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : ICategoryAttributeRuleService
{
    private readonly string _key = "CategoryAttributeRule";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICategoryAttributeRuleRepository _attributeRuleRepository = attributeRuleRepository;
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<CategoryAttributeRuleDto> CreateAsync(
        Guid companyId,
        Guid categoryId,
        CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    )
    {
        await _categoryService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == categoryId,
             ct
        );

        var entity = _mapper.Map<CategoryAttributeRule>(createDto);
        entity.CategoryId = categoryId;

        var created = await _attributeRuleRepository.AddAsync(entity, ct);

        await _attributeRuleRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryAttributeRuleDto>(created);
    }

    public async Task<CategoryAttributeRuleDto> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        await _categoryService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == categoryId,
             ct
        );

        var categoryAttributeRule = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CategoryId == categoryId && p.Id == id,
            ct
        );

        return _mapper.Map<CategoryAttributeRuleDto>(categoryAttributeRule);
    }

    public async Task<ListResponseModel<CategoryAttributeRuleDto>> FilterByQAsync(
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _attributeRuleRepository.FilterByQ(parameters);
        var res = await _attributeRuleRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategoryAttributeRuleDto>(
            results: _mapper.Map<IReadOnlyList<CategoryAttributeRuleDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<CategoryAttributeRuleDto>> GetFilteredAsync(
        CategoryAttributeRuleParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<CategoryAttributeRule>(filterNodeDto);
        var query = _attributeRuleRepository.GetFiltered(advancedFilters);
        var res = await _attributeRuleRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategoryAttributeRuleDto>(
            results: _mapper.Map<IReadOnlyList<CategoryAttributeRuleDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<CategoryAttributeRuleDto> PatchAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    )
    {
        await _categoryService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == categoryId,
             ct
        );

        var categoryAttributeRule = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CategoryId == categoryId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchCategoryAttributeRuleDto>(categoryAttributeRule);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, categoryAttributeRule);

        await _attributeRuleRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryAttributeRuleDto>(categoryAttributeRule);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        await _categoryService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == categoryId,
             ct
        );

        await _attributeRuleRepository.Remove(
            e => e.CategoryId == categoryId && e.Id == id,
            ct
        );
    }

    private async Task<CategoryAttributeRule> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<CategoryAttributeRule, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _attributeRuleRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
