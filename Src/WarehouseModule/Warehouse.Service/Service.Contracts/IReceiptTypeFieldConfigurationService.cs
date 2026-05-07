using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

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

    Task<ListResponseModel<ReceiptTypeFieldConfigurationListDto>> FilterByQAsync(
        Guid companyId,
        Guid receiptTypeId,
        ReceiptTypeFieldConfigurationParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptTypeFieldConfigurationListDto>> GetFilteredAsync(
        Guid companyId,
        Guid receiptTypeId,
        ReceiptTypeFieldConfigurationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
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
