using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IRemittanceTypeBusinessRuleValidator
{
    void ValidateParameters(RemittanceTypeParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateRemittanceTypeDto createDto,
        CancellationToken ct
    );

    Task ValidateRemittanceTypeCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedRemittanceTypeId,
        int code,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
