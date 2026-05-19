using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IRemittanceBusinessRuleValidator
{
    void ValidateParameters(RemittanceParameters parameters);

    Task ValidateCreateAsync(Guid companyId, CreateRemittanceDto createDto, CancellationToken ct);

    Task ValidateUpdateAsync(Guid companyId, Guid id, CreateRemittanceDto updateDto, CancellationToken ct);

    Task ValidateDeleteAsync(Guid companyId, Guid id, CancellationToken ct);
}
