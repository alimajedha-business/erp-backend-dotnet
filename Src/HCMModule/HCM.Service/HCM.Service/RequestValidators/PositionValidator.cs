using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class PositionValidator : AbstractValidator<Position>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public PositionValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.StatusChangeDate)
            .NotEmpty()
            .When(x => x.Status == false)
            .WithMessage(_localizer["StatusChangeDate.IsRequired"].Value);
    }
}