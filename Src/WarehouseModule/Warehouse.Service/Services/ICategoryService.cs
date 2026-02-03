using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(
        Guid companyId,
        CreateCategoryDto createCategoryDto,
        CancellationToken ct
    );
    Task<ListResponseModel<CategoryDto>> GetAllCategoriesAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
    Task<CategoryDto> GetCategoryByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<ListResponseModel<CategoryDto>> GetDirectCategoryChildrenByIdAsync(
        Guid companyId,
        Guid id,
        CategoryParameters categoryParameters,
        CancellationToken ct
    );
    Task<CategoryDto> UpdateCategoryAsync(
        Guid companyId,
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    );
    Task<bool> DeleteCategoryAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
