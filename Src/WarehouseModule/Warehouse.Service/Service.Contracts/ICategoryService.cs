using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    );

    Task<CategoryDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryDto>> GetAllAsync(
        Guid companyId,
        CategoryParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryDto>> GetAllAsync(
        Guid companyId,
        CategoryParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    );

    Task<CategoryDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}