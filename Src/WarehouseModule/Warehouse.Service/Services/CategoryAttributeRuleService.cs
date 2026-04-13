using AutoMapper;

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
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class CategoryAttributeRuleService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryAttributeRuleRepository categoryAttributeRuleRepository,
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

    public Task<ListResponseModel<CategoryAttributeRuleSlimDto>> GetAllAsync(
        Guid categoryId,
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        return GetByConditionAsync(
            parameters,
            e => e.CategoryId == categoryId,
            includeQuery,
            ct,
            filterNodeDto
        );
    }

    public override Task<CategoryAttributeRuleDto> GetByIdAsync(
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(id, includeQuery, ct);

    private static IQueryable<CategoryAttributeRule> includeQuery(
        IQueryable<CategoryAttributeRule> q
    ) => q.Include(x => x.Category).Include(c => c.Attribute);
}