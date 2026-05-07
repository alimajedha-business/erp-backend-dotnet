using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IReceiptTypeConfigurationBusinessRuleValidator
{
    void ValidateParameters(ReceiptTypeConfigurationParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptTypeConfigurationDto createDto,
        CancellationToken ct
    );
}
