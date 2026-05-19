using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IWorkLocationBusinessRuleValidator
{
    void ValidateParameters(WorkLocationParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateWorkLocationDto createDto,
        CancellationToken ct
    );

    Task ValidatePatchAsync(
        Guid companyId,
        Guid id,
        PatchWorkLocationDto patchDto,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
