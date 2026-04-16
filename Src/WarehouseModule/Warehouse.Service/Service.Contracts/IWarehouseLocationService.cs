using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IWarehouseLocationService
{
    Task<WarehouseLocationDto> CreateAsync(
        Guid warehouseId,
        CreateWarehouseLocationDto createDto,
        CancellationToken ct
    );
    Task<WarehouseLocationDto> GetByIdAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    );

    Task<ListResponseModel<WarehouseLocationListDto>> GetFilterByQAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        CancellationToken ct
    );

    Task<ListResponseModel<WarehouseLocationListDto>> GetListAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    );

    Task<WarehouseLocationDto> PatchAsync(
        Guid warehouseId,
        Guid id,
        JsonPatchDocument<PatchWarehouseLocationDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    );
}
