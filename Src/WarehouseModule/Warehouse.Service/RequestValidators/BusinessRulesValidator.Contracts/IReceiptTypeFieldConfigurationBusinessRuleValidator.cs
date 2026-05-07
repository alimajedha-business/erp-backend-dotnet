using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IReceiptTypeFieldConfigurationBusinessRuleValidator
{
    void ValidateParameters(ReceiptTypeFieldConfigurationParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        Guid receiptTypeId,
        CreateReceiptTypeFieldConfigurationDto createDto,
        CancellationToken ct
    );

    Task ValidatePatchAsync(
        Guid companyId,
        ReceiptTypeFieldConfiguration configuration,
        PatchReceiptTypeFieldConfigurationDto patchDto,
        CancellationToken ct
    );
}
