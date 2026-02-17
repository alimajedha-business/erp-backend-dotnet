using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    );
    Task<ListResponseModel<CategoryDto>> GetAllCategoriesAsync(
        Guid companyId,
        CategoryParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<CategoryDto> GetCategoryByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<CategoryDto> PatchCategoryAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    );
    Task DeleteCategoryAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
