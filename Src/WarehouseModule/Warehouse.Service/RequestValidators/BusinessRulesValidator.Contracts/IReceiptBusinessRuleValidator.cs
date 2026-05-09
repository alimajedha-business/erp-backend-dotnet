using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IReceiptBusinessRuleValidator
{
    void ValidateParameters(ReceiptParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptDto createDto,
        CancellationToken ct
    );

    Task ValidateUpdateAsync(
        Guid companyId,
        Guid id,
        CreateReceiptDto updateDto,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
