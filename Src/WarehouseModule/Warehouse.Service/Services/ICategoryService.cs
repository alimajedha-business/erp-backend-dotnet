using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createCategoryDto,
        CancellationToken ct
    );
    Task<ListResponseModel<CategoryDto>> GetListAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
    Task<CategoryDto> GetByIdAsync(Guid companyId, Guid id);
    Task<CategoryDto> UpdateAsync(
        Guid companyId,
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    );
    Task<bool> DeleteAsync(Guid companyId, Guid id);
}
