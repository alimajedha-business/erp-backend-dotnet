using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IReceiptTypeBusinessRuleValidator
{
    void ValidateParameters(ReceiptTypeParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptTypeDto createDto,
        CancellationToken ct
    );

    Task ValidateReceiptTypeCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedReceiptId,
        int code,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
