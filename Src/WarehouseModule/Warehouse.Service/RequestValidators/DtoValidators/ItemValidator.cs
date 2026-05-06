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
    }
}

public static class PatchItemPolicy
{
    private static readonly PatchFieldRule RequiredReplaceOnly =
        PatchFieldRule.RequiredReplaceOnly();

    private static readonly PatchFieldRule OptionalReplaceOrRemove =
        PatchFieldRule.OptionalReplaceOrRemove();

    private static readonly PatchFieldRule RequiredReplaceOnlyAllowEmpty =
        new(
            AllowNull: false,
            AllowEmpty: true,
            AllowRemove: false,
            AllowedOperations: [OperationType.Replace]
        );

    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = RequiredReplaceOnly,
        ["/title"] = RequiredReplaceOnly,
        ["/titleInEnglish"] = OptionalReplaceOrRemove,
        ["/technicalNumber"] = OptionalReplaceOrRemove,
        ["/barcode"] = OptionalReplaceOrRemove,
        ["/itemAttributes"] = RequiredReplaceOnlyAllowEmpty,
        ["/attributeIds"] = RequiredReplaceOnlyAllowEmpty,
        ["/itemUnitOfMeasurements"] = RequiredReplaceOnlyAllowEmpty,
        ["/secondaryUnitOfMeasurementIds"] = RequiredReplaceOnlyAllowEmpty,
        ["/itemWarehouses"] = RequiredReplaceOnlyAllowEmpty,
        ["/itemUnitOfMeasurementConversions"] = RequiredReplaceOnlyAllowEmpty,
        ["/unitConversions"] = RequiredReplaceOnlyAllowEmpty
    };

    public static void Validate(JsonPatchDocument<PatchItemDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
