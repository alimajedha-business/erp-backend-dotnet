using NGErp.Base.Service.Validators;
using NGErp.HCM.Domain.Exceptions;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidators;

public class JobBusinessRuleValidator(
    IJobRepository jobRepository
) : IJobBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "code",
        "title",
        "jobCategoryId",
        "description",
        "levelCode"
    };

    private readonly IJobRepository _jobRepository = jobRepository;

    public void ValidateParameters(JobParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateJobDto createDto,
        CancellationToken ct
    )
    {
        await ValidateJobCodeUniquenessAsync(
            companyId,
            excludedJobId: null,
            createDto.Code,
            ct
        );

        await ValidateTitleUniquenessAsync(
            companyId,
            excludedJobId: null,
            createDto.Title,
            ct
        );
    }

    public async Task ValidateJobCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedJobId,
        string code,
        CancellationToken ct
    )
    {
        var exists = excludedJobId is null
            ? await _jobRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Code == code,
                ct
            )
            : await _jobRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Id != excludedJobId.Value &&
                e.Code == code,
                ct
            );

        if (exists)
            throw new JobCodeAlreadyExistsException(code);
    }

    public async Task ValidateTitleUniquenessAsync(
        Guid companyId,
        Guid? excludedJobId,
        string title,
        CancellationToken ct
    )
    {
        var exists = excludedJobId is null
            ? await _jobRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Title == title,
                ct
            )
            : await _jobRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Id != excludedJobId.Value &&
                e.Title == title,
                ct
            );

        if (exists)
            throw new JobTitleAlreadyExistsException(title);
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _jobRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );

        if (!exists)
            throw new JobNotFoundException();
    }
}
