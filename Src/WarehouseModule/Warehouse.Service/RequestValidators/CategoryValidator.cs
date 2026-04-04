using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CategoryValidator : AbstractValidator<Category>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CategoryValidator(IStringLocalizer<WarehouseResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x)
            .Must(x => !(x.LevelNo == 1 && x.IsLastLevel))
            .WithMessage(_localizer["Category.LevelNo.NotLastLevelIf1"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value);
    }
}
