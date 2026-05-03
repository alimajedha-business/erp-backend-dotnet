using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateCategoryLevelConstraintValidator :
    AbstractValidator<CreateCategoryLevelConstraintDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateCategoryLevelConstraintValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.LevelNo)
            .InclusiveBetween(1, 6)
            .WithMessage(_localizer["CategoryLevelConstraint.LevelNo.Range"].Value);

        RuleFor(p => p.CodeLength)
            .InclusiveBetween(1, 5)
            .WithMessage(_localizer["CategoryLevelConstraint.CodeLength.Range"].Value);
    }
}

public class PatchCategoryLevelConstraintValidator :
    AbstractValidator<PatchCategoryLevelConstraintDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchCategoryLevelConstraintValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.CodeLength)
            .InclusiveBetween(1, 5)
            .WithMessage(_localizer["CategoryLevelConstraint.CodeLength.Range"].Value)
            .When(p => p.CodeLength is not null);
    }
}

public static class PatchCategoryLevelConstraintPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/codeLength"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(
        JsonPatchDocument<PatchCategoryLevelConstraintDto> patchDocument
    )
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
