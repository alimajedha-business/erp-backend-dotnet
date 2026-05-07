using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptTypeFieldConfigurationService
{
    Task<ReceiptTypeFieldConfigurationDto> CreateAsync(
        Guid companyId,
        Guid receiptTypeId,
        CreateReceiptTypeFieldConfigurationDto createDto,
        CancellationToken ct
    );

    Task<ReceiptTypeFieldConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ReceiptTypeFieldConfigurationDto> PatchAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        CancellationToken ct
    );
}
