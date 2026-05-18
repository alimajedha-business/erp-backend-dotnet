using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptSourceOfSupplyService
{
    Task<ReceiptSourceOfSupplyDto> CreateAsync(
        Guid companyId,
        CreateReceiptSourceOfSupplyDto createDto,
        CancellationToken ct
    );

    Task<ReceiptSourceOfSupplyDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptSourceOfSupplySlimDto>> FilterByQAsync(
        ReceiptSourceOfSupplyParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptSourceOfSupplyDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptSourceOfSupplyParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ReceiptSourceOfSupplyDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchReceiptSourceOfSupplyDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(Guid companyId, CancellationToken ct);
}
