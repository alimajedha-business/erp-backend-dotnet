using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators.DtoValidators;

public class CreateJobCategoryValidator : AbstractValidator<CreateJobCategoryDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public CreateJobCategoryValidator(
        IStringLocalizer<HCMResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(_localizer["JobCategory.Code.NotEmpty"].Value)
            .InclusiveBetween(1, int.MaxValue)
            .WithMessage(_localizer["JobCategory.Code.Range"].Value);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(_localizer["JobCategory.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["JobCategory.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(_localizer["JobCategory.Title.MaxLength"].Value);
    }
}

public class PatchJobCategoryValidator : AbstractValidator<PatchJobCategoryDto>
{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public PatchJobCategoryValidator(
        IStringLocalizer<HCMResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(x => x.Code)
           .NotEmpty()
           .WithMessage(_localizer["JobCategory.Code.NotEmpty"].Value)
           .InclusiveBetween(1, int.MaxValue)
           .WithMessage(_localizer["JobCategory.Code.Range"].Value)
           .When(x => x.Code is not null);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(_localizer["JobCategory.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["JobCategory.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(_localizer["JobCategory.Title.MaxLength"].Value)
            .When(x => x.Code is not null);
    }
}

public static class PatchJobCategoryPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchJobCategoryDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}