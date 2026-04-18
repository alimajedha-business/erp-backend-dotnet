using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IWarehouseTypeService
{
    Task<WarehouseTypeDto> CreateAsync(
        CreateWarehouseTypeDto createDto,
        CancellationToken ct
    );

    Task<WarehouseTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<WarehouseTypeDto>> FilterByQAsync(
        WarehouseTypeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<WarehouseTypeDto>> GetFilteredAsync(
        WarehouseTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<WarehouseTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchWarehouseTypeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(CancellationToken ct);
}
