using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators.DtoValidators;

public class CreateEmployeeWarriorRecordValidator : AbstractValidator<CreateEmployeeWarriorRecordDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmployeeWarriorRecordValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;
    }
}
