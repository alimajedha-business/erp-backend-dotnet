using FluentValidation;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IJobCategoryBusinessRulesValidator
{
    Task ValidateCreateAsync(
        CreateJobCategoryDto createDto,
        CancellationToken ct
    );
}