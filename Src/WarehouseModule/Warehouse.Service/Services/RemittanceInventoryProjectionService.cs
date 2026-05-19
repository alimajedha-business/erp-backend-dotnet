using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class RemittanceInventoryProjectionService(
    IInventoryProjectionRepository inventoryProjectionRepository
) : IRemittanceInventoryProjectionService
{
    private readonly IInventoryProjectionRepository _inventoryProjectionRepository =
        inventoryProjectionRepository;

    public Task RebuildAsync(
        Guid companyId,
        Remittance remittance,
        CancellationToken ct
    )
    {
        return remittance.Status == RemittanceStatus.Posted
            ? _inventoryProjectionRepository.RebuildRemittanceAsync(companyId, remittance.Id, ct)
            : _inventoryProjectionRepository.RemoveRemittanceAsync(companyId, remittance.Id, ct);
    }

    public Task RemoveAsync(
        Guid companyId,
        Guid remittanceId,
        CancellationToken ct
    )
    {
        return _inventoryProjectionRepository.RemoveRemittanceAsync(companyId, remittanceId, ct);
    }
}
