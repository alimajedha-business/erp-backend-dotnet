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
}