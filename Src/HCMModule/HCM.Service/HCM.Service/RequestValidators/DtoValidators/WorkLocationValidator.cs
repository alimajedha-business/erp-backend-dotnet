using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.Validators;
using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.RequestValidators.DtoValidators;

public class CreateWorkLocationValidator : AbstractValidator<CreateWorkLocationDto>
{
    public CreateWorkLocationValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(250);
    }
}

public class PatchWorkLocationValidator : AbstractValidator<PatchWorkLocationDto>
{
    public PatchWorkLocationValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(250)
            .When(x => x.Title is not null);
    }
}

public static class PatchWorkLocationPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        ["/parentId"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchWorkLocationDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
