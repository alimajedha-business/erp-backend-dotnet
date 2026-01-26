using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto category);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(
        CategoryParameters prms,
        string? search = null,
        object[]? searchPrms = null
    );
    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
    Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto category);
    Task<bool> DeleteCategoryAsync(Guid id);
}
