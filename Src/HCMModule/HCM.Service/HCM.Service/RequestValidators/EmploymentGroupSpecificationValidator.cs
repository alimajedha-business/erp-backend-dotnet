using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators;

public class CreateEmploymentGroupSpecificationValidator : AbstractValidator<CreateEmploymentGroupSpecificationDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateEmploymentGroupSpecificationValidator(IStringLocalizer<HCMResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.WorkMinutes)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(720)
            .WithMessage(_localizer["EmploymentGroupSpecification.WorkMinutes"]);

        RuleFor(x => x.MonthType)
            .NotEmpty()
            .IsInEnum()
            .WithMessage(_localizer["EmploymentGroupSpecification.MonthType"]);
    }
}