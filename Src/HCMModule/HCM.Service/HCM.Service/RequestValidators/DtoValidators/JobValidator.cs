using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.Validators;
using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.RequestValidators.DtoValidators;

public class CreateJobValidator : AbstractValidator<CreateJobDto>
{
    public CreateJobValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(500);

        RuleFor(x => x.JobCategoryId)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}

public class PatchJobValidator : AbstractValidator<PatchJobDto>
{
    public PatchJobValidator()
    {
        RuleFor(x => x.Code)
            .MaximumLength(50)
            .When(x => x.Code is not null);

        RuleFor(x => x.Title)
            .MinimumLength(2)
            .MaximumLength(500)
            .When(x => x.Title is not null);

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => x.Description is not null);
    }
}

public static class PatchJobPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/jobCategoryId"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/description"] = PatchFieldRule.OptionalReplaceOrRemove()
    };

    public static void Validate(JsonPatchDocument<PatchJobDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
