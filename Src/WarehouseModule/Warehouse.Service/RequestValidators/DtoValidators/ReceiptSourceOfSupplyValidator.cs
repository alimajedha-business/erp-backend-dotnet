using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptSourceOfSupplyValidator :
    AbstractValidator<CreateReceiptSourceOfSupplyDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateReceiptSourceOfSupplyValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptSourceOfSupply.Code.NotEmpty"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptSourceOfSupply.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["ReceiptSourceOfSupply.Title.MinLength"].Value)
            .MaximumLength(100)
            .WithMessage(_localizer["ReceiptSourceOfSupply.Title.MaxLength"].Value);
    }
}

public class PatchReceiptSourceOfSupplyValidator :
    AbstractValidator<PatchReceiptSourceOfSupplyDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchReceiptSourceOfSupplyValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptSourceOfSupply.Code.NotEmpty"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["ReceiptSourceOfSupply.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["ReceiptSourceOfSupply.Title.MinLength"].Value)
            .MaximumLength(100)
            .WithMessage(_localizer["ReceiptSourceOfSupply.Title.MaxLength"].Value)
            .When(p => p.Title is not null);
    }
}

public static class PatchReceiptSourceOfSupplyPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/isActive"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(
        JsonPatchDocument<PatchReceiptSourceOfSupplyDto> patchDocument
    )
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}