using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class CreateEmployeeRelativeValidator : AbstractValidator<CreateEmployeeRelativeDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmployeeRelativeValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;
    }
}
