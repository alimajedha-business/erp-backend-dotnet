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

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value);

        RuleFor(p => p.HasNextLevel)
            .Equal(true)
            .When(p => p.LevelNo == 1)
            .WithMessage(_localizer["Category.LevelNo.NotLastLevelIf1"].Value);

        RuleFor(p => p.HasNextLevel)
            .Equal(false)
            .When(p => p.LevelNo == 6)
            .WithMessage(_localizer["Category.LevelNo.LastLevelIf6"].Value);
    }
}
