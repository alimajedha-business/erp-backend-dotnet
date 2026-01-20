using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto category);
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(Guid id);
    Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto category);
    Task<bool> DeleteCategoryAsync(Guid id);
}
