using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class CategoryAttributeRuleService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryAttributeRuleRepository categoryAttributeRuleRepository,
    ICategoryService categoryService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        CategoryAttributeRule,
        CategoryAttributeRuleDto,
        CategoryAttributeRuleSlimDto,
        CategoryAttributeRuleParameters,
        ICategoryAttributeRuleRepository,
        WarehouseResource
    >(
        filterBuilder,
        categoryAttributeRuleRepository,
        mapper,
        localizer
    ),
    ICategoryAttributeRuleService
{
    protected override string LocalizerKey => "CategoryAttributeRule";
    private readonly ICategoryAttributeRuleRepository _categoryAttributeRuleRepository =
        categoryAttributeRuleRepository;

    private readonly ICategoryService _categoryService = categoryService;

    public async Task<CategoryAttributeRuleDto> CreateCategoryAttributeRuleAsync(
        Guid companyId,
        Guid categoryId,
        CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    )
    {
        await EnsureCategoryAsync(companyId, categoryId, ct);

        var entity = _mapper.Map<CategoryAttributeRule>(createDto);
        entity.CategoryId = categoryId;

        var created = await _categoryAttributeRuleRepository.AddAsync(entity, ct);
        await _categoryAttributeRuleRepository.SaveChangesAsync(ct);

        return _mapper.Map<CategoryAttributeRuleDto>(created);
    }

    public async Task<ListResponseModel<CategoryAttributeRuleSlimDto>> GetAllCategoryAttributeRulesAsync(
        Guid companyId,
        Guid categoryId,
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await EnsureCategoryAsync(companyId, categoryId, ct);

        Expression<Func<CategoryAttributeRule, bool>> categoryFilter = 
            rule => rule.CategoryId == categoryId;

        return await GetByConditionAsync(
            parameters,
            categoryFilter,
            includeQuery,
            ct,
            filterNodeDto
        );
    }

    public async Task<CategoryAttributeRuleDto> GetCategoryAttributeRuleByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        await EnsureCategoryAsync(companyId, categoryId, ct);
        return await GetByIdAsync(id, includeQuery, ct);
    }

    public async Task<CategoryAttributeRuleDto> PatchCategoryAttributeRuleAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDoc,
        CancellationToken ct
    )
    {
        await EnsureCategoryAsync(companyId, categoryId, ct);
        return await PatchAsync(id, patchDoc, ct);
    }

    public async Task DeleteCategoryAttributeRuleAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        await EnsureCategoryAsync(companyId, categoryId, ct);
        await DeleteAsync(id, ct);
    }

    private Task<CategoryDto> EnsureCategoryAsync(
        Guid companyId,
        Guid categoryId,
        CancellationToken ct
    ) =>  _categoryService.GetCategoryByIdAsync(companyId, categoryId, ct);

    private static IQueryable<CategoryAttributeRule> includeQuery(
        IQueryable<CategoryAttributeRule> q
    ) => q.Include(x => x.Category).Include(c => c.Attribute);
}