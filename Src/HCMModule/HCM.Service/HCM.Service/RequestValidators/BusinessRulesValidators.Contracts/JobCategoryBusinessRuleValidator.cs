using NGErp.HCM.Domain.Exceptions;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidators;

public class JobCategoryBusinessRuleValidator(
    IJobCategoryRepository jobCategoryRepository)
    : IJobCategoryBusinessRulesValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "code",
        "title",
        "levelNo",
        "createdAt"
    };

    private readonly IJobCategoryRepository _jobCategoryRepository = jobCategoryRepository;

    public async Task ValidateCreateAsync(CreateJobCategoryDto createDto, CancellationToken ct)
    {
        await ValidateCodeUniquenessAsync(
           createDto.Code,
            ct
        );
    }

    private async Task ValidateCodeUniquenessAsync(
        int code,
        CancellationToken ct
    )
    {
        var exists = await _jobCategoryRepository.AnyAsync(
            c => c.Code == code,
            ct);

        if (exists)
            throw new JobCategoryCodeAlreadyExistsException(code);
    }
}