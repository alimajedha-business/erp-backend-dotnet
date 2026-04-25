using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IItemTypeService
{
    Task<ItemTypeDto> CreateAsync(
        CreateItemTypeDto createDto,
        CancellationToken ct
    );

    Task<ItemTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ItemTypeSlimDto>> FilterByQAsync(
        ItemTypeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ItemTypeDto>> GetFilteredAsync(
        ItemTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ItemTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchItemTypeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(CancellationToken ct);
}
