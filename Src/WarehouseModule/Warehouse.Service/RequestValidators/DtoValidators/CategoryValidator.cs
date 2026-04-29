using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateCategoryValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Category.Code.NotEmpty"].Value)
            .MaximumLength(64)
            .WithMessage(_localizer["Category.Code.MaxLength"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(_localizer["Category.Title.MaxLength"].Value);

        RuleFor(p => p.LevelNo)
            .InclusiveBetween(1, 6)
            .WithMessage(_localizer["Category.LevelNo.Range"].Value);
    }
}

public class PatchCategoryValidator : AbstractValidator<PatchCategoryDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchCategoryValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Category.Code.NotEmpty"].Value)
            .MaximumLength(64)
            .WithMessage(_localizer["Category.Code.MaxLength"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(_localizer["Category.Title.MaxLength"].Value)
            .When(p => p.Title is not null);
    }
}

public static class PatchCategoryPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/hasNextLevel"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchCategoryDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}