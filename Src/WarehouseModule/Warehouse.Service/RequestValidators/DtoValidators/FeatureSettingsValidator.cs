using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
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

        RuleFor(p => p.WarehouseSerialRule)
            .IsInEnum()
            .WithMessage(_localizer["FeatureSettings.WarehouseSerialRule.Invalid"].Value);

        RuleFor(p => p.PriceRoundingPoint)
            .GreaterThanOrEqualTo(0)
            .WithMessage(_localizer["FeatureSettings.PriceRoundingPoint.Range"].Value);

        RuleFor(p => p.AccountingFiscalYear)
            .NotEmpty()
            .WithMessage(_localizer["FeatureSettings.AccountingFiscalYear.NotEmpty"].Value);

        RuleFor(p => p.PropertyFiscalYear)
            .NotEmpty()
            .WithMessage(_localizer["FeatureSettings.PropertyFiscalYear.NotEmpty"].Value);

        RuleFor(p => p.WarehousePreviousFiscalYear)
            .NotEmpty()
            .WithMessage(_localizer["FeatureSettings.WarehousePreviousFiscalYear.NotEmpty"].Value);
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

        RuleFor(p => p.WarehouseSerialRule)
            .Must(p => p is null || Enum.IsDefined(typeof(WarehouseSerialRule), p.Value))
            .WithMessage(_localizer["FeatureSettings.WarehouseSerialRule.Invalid"].Value);

        RuleFor(p => p.PriceRoundingPoint)
            .GreaterThanOrEqualTo(0)
            .WithMessage(_localizer["FeatureSettings.PriceRoundingPoint.Range"].Value)
            .When(p => p.PriceRoundingPoint is not null);

        RuleFor(p => p.AccountingFiscalYear)
            .NotEmpty()
            .WithMessage(_localizer["FeatureSettings.AccountingFiscalYear.NotEmpty"].Value)
            .When(p => p.AccountingFiscalYear is not null);

        RuleFor(p => p.PropertyFiscalYear)
            .NotEmpty()
            .WithMessage(_localizer["FeatureSettings.PropertyFiscalYear.NotEmpty"].Value)
            .When(p => p.PropertyFiscalYear is not null);

        RuleFor(p => p.WarehousePreviousFiscalYear)
            .NotEmpty()
            .WithMessage(_localizer["FeatureSettings.WarehousePreviousFiscalYear.NotEmpty"].Value)
            .When(p => p.WarehousePreviousFiscalYear is not null);
    }
}

public static class PatchFeatureSettingsPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/maxCategoryLevel"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/warehouseSerialRule"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/priceRoundingPoint"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/accountingFiscalYear"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/propertyFiscalYear"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/warehousePreviousFiscalYear"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(
        JsonPatchDocument<PatchFeatureSettingsDto> patchDocument
    )
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
