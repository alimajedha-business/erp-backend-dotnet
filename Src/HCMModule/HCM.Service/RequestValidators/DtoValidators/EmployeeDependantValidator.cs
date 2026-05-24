using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators.DtoValidators;

public class CreateEmployeeDependantValidator : AbstractValidator<CreateEmployeeDependantDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmployeeDependantValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;
    }
}
