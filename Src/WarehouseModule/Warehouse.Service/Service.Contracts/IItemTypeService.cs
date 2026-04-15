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

    Task<ListResponseModel<ItemTypeDto>> GetAllAsync(
        ItemTypeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ItemTypeDto>> GetAllAsync(
        ItemTypeParameters parameters,
        FilterNodeDto filterNodeDto,
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
