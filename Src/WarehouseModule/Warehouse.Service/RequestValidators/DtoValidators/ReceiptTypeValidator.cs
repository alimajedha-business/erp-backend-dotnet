using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptTypeValidator : AbstractValidator<CreateReceiptTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateReceiptTypeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptType.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["ReceiptType.Code.Numeric"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptType.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["ReceiptType.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["ReceiptType.Title.MaxLength"].Value);

        RuleFor(p => p.AddToStock)
            .NotNull()
            .WithMessage(_localizer["ReceiptType.AddToStock.NotNull"].Value);
    }
}

public class PatchReceiptTypeValidator : AbstractValidator<PatchReceiptTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchReceiptTypeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptType.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["ReceiptType.Code.Numeric"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptType.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["ReceiptType.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["ReceiptType.Title.MaxLength"].Value)
            .When(p => p.Title is not null);
    }
}

public static class PatchReceiptTypePolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/addToStock"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchReceiptTypeDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
