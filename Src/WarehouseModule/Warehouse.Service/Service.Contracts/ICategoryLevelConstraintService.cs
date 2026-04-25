using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface ICategoryLevelConstraintService
{
    Task<CategoryLevelConstraintDto> CreateAsync(
        Guid companyId,
        CreateCategoryLevelConstraintDto createDto,
        CancellationToken ct
    );

    Task<CategoryLevelConstraintDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<CategoryLevelConstraintDto>> GetFilteredAsync(
        Guid companyId,
        CategoryLevelConstraintParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<CategoryLevelConstraintDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryLevelConstraintDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<CategoryLevelConstraintDto> GetByLevelNoAsync(
        Guid companyId,
        int levelNo,
        CancellationToken ct
    );

    Task<int> GetNextLevelAsync(
        Guid companyId,
        CancellationToken ct
    );
}
