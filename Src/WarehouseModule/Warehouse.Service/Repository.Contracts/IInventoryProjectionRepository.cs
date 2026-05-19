using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Repository.Contracts;

public interface IInventoryProjectionRepository
{
    Task RebuildReceiptAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    );

    Task RemoveReceiptAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    );

    Task RebuildRemittanceAsync(
        Guid companyId,
        Guid remittanceId,
        CancellationToken ct
    );

    Task RemoveRemittanceAsync(
        Guid companyId,
        Guid remittanceId,
        CancellationToken ct
    );

    Task<IReadOnlyList<WarehouseLocationUsage>> GetLocationUsagesAsync(
        Guid companyId,
        IReadOnlyCollection<Guid> warehouseLocationIds,
        CancellationToken ct
    );
}
