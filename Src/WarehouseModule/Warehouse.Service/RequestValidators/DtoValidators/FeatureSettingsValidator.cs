using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateFeatureSettingsValidator : AbstractValidator<CreateFeatureSettingsDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateFeatureSettingsValidator(IStringLocalizer<WarehouseResource> localizer)
    {
        _localizer = localizer;

        RuleFor(p => p.MaxCategoryLevel)
            .InclusiveBetween(2, 6)
            .WithMessage(_localizer["Category.LevelNo.Range"].Value);
    }
}

public class PatchFeatureSettingsValidator : AbstractValidator<PatchFeatureSettingsDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchFeatureSettingsValidator(IStringLocalizer<WarehouseResource> localizer)
    {
        _localizer = localizer;

        RuleFor(p => p.MaxCategoryLevel)
            .InclusiveBetween(2, 6)
            .WithMessage(_localizer["Category.LevelNo.Range"].Value)
            .When(p => p.MaxCategoryLevel is not null);
    }
}