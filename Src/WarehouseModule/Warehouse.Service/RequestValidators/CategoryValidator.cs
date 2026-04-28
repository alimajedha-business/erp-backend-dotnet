using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

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
            .WithMessage(_localizer["Category.Code.MaxLength"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(_localizer["Category.Title.MaxLength"].Value);

        RuleFor(p => p.HasNextLevel)
            .NotNull()
            .WithMessage(_localizer["Category.HasNextLevel.NotNull"].Value);
    }
}
