using NGErp.Base.Service.Validators;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidators;

public class PositionJobBusinessRuleValidator(
    IPositionJobRepository positionJobRepository
) : IPositionJobBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "positionId",
        "jobId"
    };

    private readonly IPositionJobRepository _positionJobRepository = positionJobRepository;

    public void ValidateParameters(PositionJobParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public Task ValidateCreateAsync(
        Guid companyId,
        CreatePositionJobDto createDto,
        CancellationToken ct
    )
    {
        _ = companyId;
        _ = createDto;
        _ = ct;
        return Task.CompletedTask;
    }

    public Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        _ = companyId;
        _ = id;
        _ = ct;
        return Task.CompletedTask;
    }
}
