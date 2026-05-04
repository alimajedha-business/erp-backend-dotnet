using FluentValidation;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IJobCategoryBusinessRulesValidator
{
    void ValidateParameters(JobCategoryParameters parameters);

    Task ValidateCreateAsync(
        CreateJobCategoryDto createDto,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
    Guid id,
    CancellationToken ct
);
}