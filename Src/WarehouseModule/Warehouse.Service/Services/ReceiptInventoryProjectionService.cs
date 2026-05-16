using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptInventoryProjectionService(
    IInventoryProjectionRepository inventoryProjectionRepository
) : IReceiptInventoryProjectionService
{
    private readonly IInventoryProjectionRepository _inventoryProjectionRepository =
        inventoryProjectionRepository;

    public Task RebuildAsync(
        Guid companyId,
        Receipt receipt,
        CancellationToken ct
    )
    {
        return receipt.Status == ReceiptStatus.Posted
            ? _inventoryProjectionRepository.RebuildReceiptAsync(companyId, receipt.Id, ct)
            : _inventoryProjectionRepository.RemoveReceiptAsync(companyId, receipt.Id, ct);
    }

    public Task RemoveAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    )
    {
        return _inventoryProjectionRepository.RemoveReceiptAsync(companyId, receiptId, ct);
    }
}
