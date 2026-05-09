using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptService
{
    Task<ReceiptDto> CreateAsync(
        Guid companyId,
        CreateReceiptDto createDto,
        CancellationToken ct
    );

    Task<ReceiptDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );
}
