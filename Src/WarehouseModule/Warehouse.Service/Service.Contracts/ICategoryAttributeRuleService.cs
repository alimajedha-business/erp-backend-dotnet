using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface ICategoryAttributeRuleService
{
    Task<CategoryAttributeRuleDto> CreateAsync(
        Guid categoryId,
        CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    );

    Task<CategoryAttributeRuleDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryAttributeRuleDto>> GetAllAsync(
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryAttributeRuleDto>> GetAllAsync(
        CategoryAttributeRuleParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<CategoryAttributeRuleDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
