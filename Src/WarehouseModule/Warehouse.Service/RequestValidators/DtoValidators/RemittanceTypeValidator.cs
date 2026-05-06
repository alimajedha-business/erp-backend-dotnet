using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateRemittanceTypeValidator : AbstractValidator<CreateRemittanceTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateRemittanceTypeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["RemittanceType.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["RemittanceType.Code.Numeric"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["RemittanceType.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["RemittanceType.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["RemittanceType.Title.MaxLength"].Value);

        RuleFor(p => p.AddToStock)
            .NotNull()
            .WithMessage(_localizer["RemittanceType.AddToStock.NotNull"].Value);
    }
}

public class PatchRemittanceTypeValidator : AbstractValidator<PatchRemittanceTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchRemittanceTypeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["RemittanceType.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["RemittanceType.Code.Numeric"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["RemittanceType.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["RemittanceType.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["RemittanceType.Title.MaxLength"].Value)
            .When(p => p.Title is not null);
    }
}

public static class PatchRemittanceTypePolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/addToStock"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchRemittanceTypeDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
