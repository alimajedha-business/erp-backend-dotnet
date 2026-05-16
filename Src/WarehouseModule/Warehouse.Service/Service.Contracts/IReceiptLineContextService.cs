using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptLineContextService
{
    Task<ReceiptLineItemContextDto> GetAsync(
        Guid companyId,
        Guid itemId,
        CancellationToken ct
    );
}
