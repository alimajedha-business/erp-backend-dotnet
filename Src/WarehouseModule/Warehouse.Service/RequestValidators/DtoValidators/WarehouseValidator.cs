using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateWarehouseValidator : AbstractValidator<CreateWarehouseDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateWarehouseValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["Warehouse.Code.Numeric"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Warehouse.Title.MinLength"].Value)
            .MaximumLength(250)
            .WithMessage(_localizer["Warehouse.Title.MaxLength"].Value);

        RuleFor(p => p.MaxMonetaryValue)
            .PrecisionScale(22, 4, false)
            .WithMessage(_localizer["Warehouse.MaxMonetaryValue.Precision"].Value);

        RuleFor(p => p.WarehouseTypeId)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Type.NotEmpty"].Value);

        RuleFor(p => p.CompanyUnitId)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.CompanyUnit.NotEmpty"].Value);
    }
}

public class PatchWarehouseValidator : AbstractValidator<PatchWarehouseDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchWarehouseValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["Warehouse.Code.Numeric"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Warehouse.Title.MinLength"].Value)
            .MaximumLength(250)
            .WithMessage(_localizer["Warehouse.Title.MaxLength"].Value)
            .When(p => p.Title is not null);

        RuleFor(p => p.MaxMonetaryValue)
            .PrecisionScale(22, 4, false)
            .WithMessage(_localizer["Warehouse.MaxMonetaryValue.Precision"].Value)
            .When(p => p.MaxMonetaryValue is not null);

        RuleFor(p => p.WarehouseTypeId)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Type.NotEmpty"].Value)
            .When(p => p.WarehouseTypeId is not null);

        RuleFor(p => p.CompanyUnitId)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.CompanyUnit.NotEmpty"].Value)
            .When(p => p.CompanyUnitId is not null);
    }
}

public static class PatchWarehousePolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/maxMonetaryValue"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/warehouseTypeId"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/companyUnitId"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/warehouseSlaveAccountCompanyId"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/warehouseAccountMasterValue"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/warehouseAccountSlaveValue"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/warehouseAccountDetailed1Value"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/warehouseAccountDetailed2Value"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/returnFromPurchaseSlaveAccountCompanyId"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/returnFromPurchaseAccountMasterValue"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/returnFromPurchaseAccountSlaveValue"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/returnFromPurchaseAccountDetailed1Value"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/returnFromPurchaseAccountDetailed2Value"] = PatchFieldRule.OptionalReplaceOrRemove()
    };

    public static void Validate(JsonPatchDocument<PatchWarehouseDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
