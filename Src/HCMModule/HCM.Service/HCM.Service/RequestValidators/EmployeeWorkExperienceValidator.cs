using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class CreateEmployeeWorkExperienceValidator : AbstractValidator<CreateEmployeeWorkExperienceDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmployeeWorkExperienceValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;
    }
}
