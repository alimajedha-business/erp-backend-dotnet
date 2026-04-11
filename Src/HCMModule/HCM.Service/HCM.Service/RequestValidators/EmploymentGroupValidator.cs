using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class CreateEmploymentGroupValidator : AbstractValidator<CreateEmploymentGroupDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmploymentGroupValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}