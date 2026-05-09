using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptTypeFieldConfigurationService
{
    Task<ReceiptTypeFieldConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );
}
