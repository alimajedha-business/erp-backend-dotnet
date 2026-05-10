using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptValidator : AbstractValidator<CreateReceiptDto>
{
    public CreateReceiptValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.Number)
            .NotEmpty()
            .WithMessage(localizer["Receipt.Number.NotEmpty"].Value);

        RuleFor(e => e.ReceiptDate)
            .NotEmpty()
            .WithMessage(localizer["Receipt.ReceiptDate.NotEmpty"].Value);

        RuleFor(e => e.ReceiptTypeId)
            .NotEmpty()
            .WithMessage(localizer["Receipt.ReceiptType.NotEmpty"].Value);

        RuleFor(e => e.ReceiptLines)
            .NotNull()
            .WithMessage(localizer["Receipt.ReceiptLines.NotEmpty"].Value)
            .Must(e => e is not null && e.Count != 0)
            .WithMessage(localizer["Receipt.ReceiptLines.NotEmpty"].Value);

        RuleFor(e => e.ReceiptFieldValues)
            .NotNull()
            .WithMessage(localizer["Receipt.ReceiptFieldValues.NotNull"].Value);

        RuleForEach(e => e.ReceiptFieldValues)
            .SetValidator(new CreateReceiptFieldValueValidator(localizer))
            .When(e => e.ReceiptFieldValues is not null);

        RuleForEach(e => e.ReceiptLines)
            .SetValidator(new CreateReceiptLineValidator(localizer))
            .When(e => e.ReceiptLines is not null);
    }
}

public class PatchReceiptValidator : AbstractValidator<PatchReceiptDto>
{
    public PatchReceiptValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.Number)
            .NotEmpty()
            .WithMessage(localizer["Receipt.Number.NotEmpty"].Value)
            .When(e => e.Number is not null);

        RuleFor(e => e.ReceiptDate)
            .NotEmpty()
            .WithMessage(localizer["Receipt.ReceiptDate.NotEmpty"].Value)
            .When(e => e.ReceiptDate is not null);

        RuleFor(e => e.ReceiptTypeId)
            .NotEmpty()
            .WithMessage(localizer["Receipt.ReceiptType.NotEmpty"].Value)
            .When(e => e.ReceiptTypeId is not null);

        RuleFor(e => e.ReceiptLines)
            .NotNull()
            .WithMessage(localizer["Receipt.ReceiptLines.NotEmpty"].Value)
            .Must(e => e is not null && e.Count != 0)
            .WithMessage(localizer["Receipt.ReceiptLines.NotEmpty"].Value)
            .When(e => e.ReceiptLines is not null);

        RuleFor(e => e.ReceiptFieldValues)
            .NotNull()
            .WithMessage(localizer["Receipt.ReceiptFieldValues.NotNull"].Value)
            .When(e => e.ReceiptFieldValues is not null);

        RuleForEach(e => e.ReceiptFieldValues)
            .SetValidator(new CreateReceiptFieldValueValidator(localizer))
            .When(e => e.ReceiptFieldValues is not null);

        RuleForEach(e => e.ReceiptLines)
            .SetValidator(new CreateReceiptLineValidator(localizer))
            .When(e => e.ReceiptLines is not null);
    }
}

public class CreateReceiptLineValidator : AbstractValidator<CreateReceiptLineDto>
{
    public CreateReceiptLineValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.ItemId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLine.Item.NotEmpty"].Value);

        RuleFor(e => e.UnitOfMeasurementId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLine.UnitOfMeasurement.NotEmpty"].Value);

        RuleFor(e => e.Quantity)
            .GreaterThan(0)
            .WithMessage(localizer["ReceiptLine.Quantity.Positive"].Value);

        RuleFor(e => e.ReceiptLineAttributeValues)
            .NotNull()
            .WithMessage(localizer["ReceiptLine.AttributeValues.NotNull"].Value);

        RuleFor(e => e.ReceiptFieldValues)
            .NotNull()
            .WithMessage(localizer["ReceiptLine.FieldValues.NotNull"].Value);

        RuleForEach(e => e.ReceiptLineAttributeValues)
            .SetValidator(new CreateReceiptLineAttributeValueValidator(localizer))
            .When(e => e.ReceiptLineAttributeValues is not null);

        RuleForEach(e => e.ReceiptFieldValues)
            .SetValidator(new CreateReceiptFieldValueValidator(localizer))
            .When(e => e.ReceiptFieldValues is not null);
    }
}

public class CreateReceiptFieldValueValidator :
    AbstractValidator<CreateReceiptFieldValueDto>
{
    public CreateReceiptFieldValueValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.FieldDefinitionId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptFieldValue.FieldDefinition.NotEmpty"].Value);
    }
}

public class CreateReceiptLineAttributeValueValidator :
    AbstractValidator<CreateReceiptLineAttributeValueDto>
{
    public CreateReceiptLineAttributeValueValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.ItemAttributeId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLineAttributeValue.ItemAttribute.NotEmpty"].Value);
    }
}

public static class PatchReceiptPolicy
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
        ["/number"] = RequiredReplaceOnly,
        ["/receiptDate"] = RequiredReplaceOnly,
        ["/receiptTypeId"] = RequiredReplaceOnly,
        ["/description"] = OptionalReplaceOrRemove,
        ["/receiptFieldValues"] = RequiredReplaceOnlyAllowEmpty,
        ["/receiptLines"] = RequiredReplaceOnlyAllowEmpty
    };

    public static void Validate(JsonPatchDocument<PatchReceiptDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
