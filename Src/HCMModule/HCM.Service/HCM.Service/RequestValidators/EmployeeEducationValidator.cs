using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class CreateEmployeeEducationValidator : AbstractValidator<CreateEmployeeEducationDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmployeeEducationValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;
    }
}
