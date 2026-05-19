using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IRemittanceInventoryProjectionService
{
    Task RebuildAsync(
        Guid companyId,
        Remittance remittance,
        CancellationToken ct
    );

    Task RemoveAsync(
        Guid companyId,
        Guid remittanceId,
        CancellationToken ct
    );
}
