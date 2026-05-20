using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IRemittanceTypeConfigurationService
{
    Task CreateAsync(
        Guid companyId,
        CreateRemittanceTypeConfigurationDto createDto,
        CancellationToken ct
    );

    Task<RemittanceTypeConfigurationDto?> GetByRemittanceTypeIdAsync(
        Guid companyId,
        Guid remittanceTypeId,
        bool trackChanges = false,
        CancellationToken ct = default
    );
}
