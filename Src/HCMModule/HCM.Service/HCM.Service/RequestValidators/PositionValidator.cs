using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class PositionChangeStatusValidator : AbstractValidator<PositionChangeStatusDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public PositionChangeStatusValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.Date)
            .NotEmpty()
            .When(x => x.Status == false)
            .WithMessage(_localizer["StatusChangeDate.IsRequired"].Value);
    }
}