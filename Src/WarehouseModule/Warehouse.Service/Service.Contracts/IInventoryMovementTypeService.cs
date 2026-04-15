using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IInventoryMovementTypeService
{
    Task<InventoryMovementTypeDto> CreateAsync(
        Guid companyId,
        CreateInventoryMovementTypeDto createDto,
        CancellationToken ct
    );

    Task<InventoryMovementTypeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<InventoryMovementTypeDto>> GetAllAsync(
        Guid companyId,
        InventoryMovementTypeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<InventoryMovementTypeDto>> GetAllAsync(
        Guid companyId,
        InventoryMovementTypeParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    );

    Task<InventoryMovementTypeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchInventoryMovementTypeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(Guid companyId, CancellationToken ct);
}
