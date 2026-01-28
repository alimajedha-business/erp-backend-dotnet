using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto, CancellationToken ct);
    Task<IEnumerable<CategoryDto>> GetPaginatedAsync(
        CategoryParameters categoryParameters,
        string? search = null,
        object[]? searchParameters = null
    );
    Task<CategoryDto?> GetByIdAsync(Guid id);
    Task<CategoryDto?> UpdateAsync(
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    );
    Task<bool> DeleteAsync(Guid id);
}
