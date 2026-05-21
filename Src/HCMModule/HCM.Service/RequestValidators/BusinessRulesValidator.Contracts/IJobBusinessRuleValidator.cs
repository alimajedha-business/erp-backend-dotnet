using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IJobBusinessRuleValidator
{
    void ValidateParameters(JobParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateJobDto createDto,
        CancellationToken ct
    );

    Task ValidateJobCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedJobId,
        string code,
        CancellationToken ct
    );

    Task ValidateTitleUniquenessAsync(
        Guid companyId,
        Guid? excludedJobId,
        string title,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
