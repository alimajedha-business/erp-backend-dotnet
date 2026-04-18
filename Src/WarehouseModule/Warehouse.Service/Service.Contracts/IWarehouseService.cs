using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IWarehouseService
{
    Task<WarehouseDto> CreateAsync(
        Guid companyId,
        CreateWarehouseDto createDto,
        CancellationToken ct
    );

    Task<WarehouseDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<WarehouseListDto>> FilterByQAsync(
        Guid companyId,
        WarehouseParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<WarehouseListDto>> GetFilteredAsync(
        Guid companyId,
        WarehouseParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<WarehouseDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchWarehouseDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(Guid companyId, CancellationToken ct);
}