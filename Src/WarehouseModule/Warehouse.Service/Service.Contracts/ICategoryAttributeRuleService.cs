using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface ICategoryAttributeRuleService
{
    Task<CategoryAttributeRuleDto> CreateAsync(
        Guid companyId,
        Guid categoryId,
        CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    );

    Task<CategoryAttributeRuleDto> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryAttributeRuleDto>> FilterByQAsync(
        CategoryAttributeRuleParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryAttributeRuleDto>> GetFilteredAsync(
        CategoryAttributeRuleParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<CategoryAttributeRuleDto> PatchAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    );
}
