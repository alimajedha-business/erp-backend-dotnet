using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateItemValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Item.Code.NotEmpty"].Value)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.Code.MaxLength"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Item.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Item.Title.MinLength"].Value)
            .MaximumLength(250)
            .WithMessage(_localizer["Item.Title.MaxLength"].Value);

        RuleFor(p => p.TitleInEnglish)
            .MaximumLength(250)
            .WithMessage(_localizer["Item.TitleInEnglish.MaxLength"].Value)
            .When(p => p.TitleInEnglish is not null);

        RuleFor(p => p.TechnicalNumber)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.TechnicalNumber.MaxLength"].Value)
            .When(p => p.TechnicalNumber is not null);

        RuleFor(p => p.Barcode)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.Barcode.MaxLength"].Value)
            .When(p => p.Barcode is not null);

        RuleFor(p => p.Sku)
            .NotEmpty()
            .WithMessage(_localizer["Item.Sku.NotEmpty"].Value)
            .MinimumLength(7)
            .WithMessage(_localizer["Item.Sku.MinLength"].Value)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.Sku.MaxLength"].Value);

        RuleFor(p => p.Weight)
            .GreaterThan(0)
            .When(p => p.Weight.HasValue);

        RuleFor(p => p.Length)
            .GreaterThan(0)
            .When(p => p.Length.HasValue);

        RuleFor(p => p.Width)
            .GreaterThan(0)
            .When(p => p.Width.HasValue);

        RuleFor(p => p.Height)
            .GreaterThan(0)
            .When(p => p.Height.HasValue);

        RuleFor(p => p.Volume)
            .GreaterThan(0)
            .When(p => p.Volume.HasValue);

        RuleFor(p => p.PreferredMassUnitId)
            .NotEmpty()
            .When(p => p.Weight.HasValue);

        RuleFor(p => p.PreferredLengthUnitId)
            .NotEmpty()
            .When(p => p.Length.HasValue || p.Width.HasValue || p.Height.HasValue);

        RuleFor(p => p.PreferredVolumeUnitId)
            .NotEmpty()
            .When(p => p.Volume.HasValue);

        RuleFor(p => p.ItemUnitOfMeasurements)
            .NotEmpty();

        RuleFor(p => p.ItemUnitOfMeasurements)
            .Must(uoms => uoms.Select(e => e.UnitOfMeasurementId).Distinct().Count() == uoms.Count)
            .WithMessage("Duplicate unitOfMeasurementId is not allowed.");

        RuleFor(p => p.ItemUnitOfMeasurements)
            .Must(uoms => uoms.Select(e => e.UnitOrder).Distinct().Count() == uoms.Count)
            .WithMessage("Duplicate unitOrder is not allowed.");

        RuleForEach(p => p.ItemUnitOfMeasurements)
            .SetValidator(new CreateItemUnitOfMeasurementValidator());
    }
}

public class PatchItemValidator : AbstractValidator<PatchItemDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchItemValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Item.Code.NotEmpty"].Value)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.Code.MaxLength"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Item.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Item.Title.MinLength"].Value)
            .MaximumLength(250)
            .WithMessage(_localizer["Item.Title.MaxLength"].Value)
            .When(p => p.Title is not null);

        RuleFor(p => p.TitleInEnglish)
            .MaximumLength(250)
            .WithMessage(_localizer["Item.TitleInEnglish.MaxLength"].Value)
            .When(p => p.TitleInEnglish is not null);

        RuleFor(p => p.TechnicalNumber)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.TechnicalNumber.MaxLength"].Value)
            .When(p => p.TechnicalNumber is not null);

        RuleFor(p => p.Barcode)
            .MaximumLength(80)
            .WithMessage(_localizer["Item.Barcode.MaxLength"].Value)
            .When(p => p.Barcode is not null);

        RuleFor(p => p.Weight)
            .GreaterThan(0)
            .When(p => p.Weight.HasValue);

        RuleFor(p => p.Length)
            .GreaterThan(0)
            .When(p => p.Length.HasValue);

        RuleFor(p => p.Width)
            .GreaterThan(0)
            .When(p => p.Width.HasValue);

        RuleFor(p => p.Height)
            .GreaterThan(0)
            .When(p => p.Height.HasValue);

        RuleFor(p => p.Volume)
            .GreaterThan(0)
            .When(p => p.Volume.HasValue);

        RuleFor(p => p.PreferredMassUnitId)
            .NotEmpty()
            .When(p => p.Weight.HasValue);

        RuleFor(p => p.PreferredLengthUnitId)
            .NotEmpty()
            .When(p => p.Length.HasValue || p.Width.HasValue || p.Height.HasValue);

        RuleFor(p => p.PreferredVolumeUnitId)
            .NotEmpty()
            .When(p => p.Volume.HasValue);

        RuleFor(p => p.ItemUnitOfMeasurements)
            .NotEmpty()
            .When(p => p.ItemUnitOfMeasurements is not null);

        RuleFor(p => p.ItemUnitOfMeasurements!)
            .Must(uoms => uoms.Select(e => e.UnitOfMeasurementId).Distinct().Count() == uoms.Count)
            .WithMessage("Duplicate unitOfMeasurementId is not allowed.")
            .When(p => p.ItemUnitOfMeasurements is not null);

        RuleFor(p => p.ItemUnitOfMeasurements!)
            .Must(uoms => uoms.Select(e => e.UnitOrder).Distinct().Count() == uoms.Count)
            .WithMessage("Duplicate unitOrder is not allowed.")
            .When(p => p.ItemUnitOfMeasurements is not null);

        RuleForEach(p => p.ItemUnitOfMeasurements)
            .SetValidator(new CreateItemUnitOfMeasurementValidator())
            .When(p => p.ItemUnitOfMeasurements is not null);
    }
}

public static class PatchItemPolicy
{
    private static readonly PatchFieldRule RequiredReplaceOnly =
        PatchFieldRule.RequiredReplaceOnly();

    private static readonly PatchFieldRule OptionalScalarValue =
        new(
            AllowNull: true,
            AllowEmpty: true,
            AllowRemove: true,
            AllowedOperations: [
                OperationType.Add,
                OperationType.Replace,
                OperationType.Remove
            ]
        );

    private static readonly PatchFieldRule OptionalCollectionAllowEmpty =
        new(
            AllowNull: false,
            AllowEmpty: true,
            AllowRemove: false,
            AllowedOperations: [OperationType.Add, OperationType.Replace]
        );

    private static readonly PatchFieldRule RequiredNonEmptyCollectionReplaceOnly =
        new(
            AllowNull: false,
            AllowEmpty: false,
            AllowRemove: false,
            AllowedOperations: [OperationType.Replace]
        );

    private static readonly Dictionary<string, PatchFieldRule> Rules =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["/code"] = RequiredReplaceOnly,
            ["/title"] = RequiredReplaceOnly,
            ["/titleInEnglish"] = OptionalScalarValue,
            ["/technicalNumber"] = OptionalScalarValue,
            ["/barcode"] = OptionalScalarValue,
            ["/isActive"] = RequiredReplaceOnly,
            ["/itemTypeId"] = RequiredReplaceOnly,
            ["/weight"] = OptionalScalarValue,
            ["/length"] = OptionalScalarValue,
            ["/width"] = OptionalScalarValue,
            ["/height"] = OptionalScalarValue,
            ["/volume"] = OptionalScalarValue,
            ["/preferredMassUnitId"] = OptionalScalarValue,
            ["/preferredLengthUnitId"] = OptionalScalarValue,
            ["/preferredVolumeUnitId"] = OptionalScalarValue,
            ["/itemAttributes"] = OptionalCollectionAllowEmpty,
            ["/attributeIds"] = OptionalCollectionAllowEmpty,
            ["/itemUnitOfMeasurements"] = RequiredNonEmptyCollectionReplaceOnly,
            ["/itemWarehouses"] = OptionalCollectionAllowEmpty,
            ["/itemUnitOfMeasurementConversions"] = OptionalCollectionAllowEmpty,
            ["/unitConversions"] = OptionalCollectionAllowEmpty
        };

    public static void Validate(JsonPatchDocument<PatchItemDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
