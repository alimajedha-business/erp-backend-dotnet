using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryAttributeRuleService
{
    Task<CategoryAttributeRuleDto> CreateCategoryAttributeRuleAsync(
        Guid companyId,
        Guid categoryId,
        CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    );
    Task<ListResponseModel<CategoryAttributeRuleSlimDto>> GetAllCategoryAttributeRulesAsync(
        Guid companyId,
        Guid categoryId,
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
   );
    Task<CategoryAttributeRuleDto> GetCategoryAttributeRuleByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
   );
    Task<CategoryAttributeRuleDto> PatchCategoryAttributeRuleAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDoc,
        CancellationToken ct
   );
    Task DeleteCategoryAttributeRuleAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
   );
}
