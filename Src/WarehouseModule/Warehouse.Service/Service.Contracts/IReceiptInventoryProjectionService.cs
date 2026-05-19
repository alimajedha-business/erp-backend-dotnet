using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptInventoryProjectionService
{
    Task RebuildAsync(
        Guid companyId,
        Receipt receipt,
        CancellationToken ct
    );

    Task RemoveAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    );
}
