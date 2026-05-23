using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.Validators;
using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.RequestValidators.DtoValidators;

public class CreatePositionJobValidator : AbstractValidator<CreatePositionJobDto>
{
    public CreatePositionJobValidator()
    {
        RuleFor(x => x.PositionId)
            .NotEmpty();

        RuleFor(x => x.JobId)
            .NotEmpty();
    }
}

public class PatchPositionJobValidator : AbstractValidator<PatchPositionJobDto>
{
    public PatchPositionJobValidator()
    {
        // Add patch rules here.
    }
}

public static class PatchPositionJobPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/positionId"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/jobId"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchPositionJobDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
