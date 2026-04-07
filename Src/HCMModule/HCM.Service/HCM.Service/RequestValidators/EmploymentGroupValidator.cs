using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class EmploymentGroupValidator : AbstractValidator<EmploymentGroup>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public EmploymentGroupValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}