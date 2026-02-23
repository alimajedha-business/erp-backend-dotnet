using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryAttributeRuleService : IBaseService<
    CategoryAttributeRule,
    CategoryAttributeRuleDto,
    CategoryAttributeRuleSlimDto,
    CategoryAttributeRuleParameters,
    ICategoryAttributeRuleRepository,
    WarehouseResource
>
{
    Task<ListResponseModel<CategoryAttributeRuleSlimDto>> GetAllAsync(
        Guid categoryId,
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
}
