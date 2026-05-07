using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IPositionJobBusinessRuleValidator
{
    void ValidateParameters(PositionJobParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreatePositionJobDto createDto,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
