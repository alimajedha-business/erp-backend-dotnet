using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IRemittanceTypeConfigurationBusinessRuleValidator
{
    void ValidateParameters(RemittanceTypeConfigurationParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateRemittanceTypeConfigurationDto createDto,
        CancellationToken ct
    );
}
