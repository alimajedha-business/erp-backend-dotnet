using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IReceiptSourceOfSupplyBusinessRuleValidator
{
    void ValidateParameters(ReceiptSourceOfSupplyParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptSourceOfSupplyDto createDto,
        CancellationToken ct
    );

    Task ValidateReceiptSourceOfSupplyCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedReceiptSourceOfSupplyId,
        int code,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
