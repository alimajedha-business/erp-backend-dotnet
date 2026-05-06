using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class CreatePositionJobValidator : AbstractValidator<CreatePositionJobDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreatePositionJobValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;
    }
}
