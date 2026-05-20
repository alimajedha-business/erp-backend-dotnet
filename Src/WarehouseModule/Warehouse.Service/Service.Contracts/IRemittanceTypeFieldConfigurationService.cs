using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IRemittanceTypeFieldConfigurationService
{
    Task<RemittanceTypeFieldConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid remittanceTypeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );
}
