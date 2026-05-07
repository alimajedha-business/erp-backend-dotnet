using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptTypeFieldConfigurationValidator :
    AbstractValidator<CreateReceiptTypeFieldConfigurationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateReceiptTypeFieldConfigurationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.FieldDefinitionId)
            .NotEmpty()
            .WithMessage(
                _localizer["ReceiptTypeFieldConfiguration.FieldDefinition.NotEmpty"].Value
            );

        RuleFor(p => p.Exists)
            .NotNull()
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Exists.NotNull"].Value);

        RuleFor(p => p.IsRequired)
            .NotNull()
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.IsRequired.NotNull"].Value);

        RuleFor(p => p.Placement)
            .NotNull()
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Placement.NotNull"].Value)
            .Must(p => p is null || Enum.IsDefined(typeof(ReceiptConfiguredPlacement), p.Value))
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Placement.Invalid"].Value);
    }
}

public class PatchReceiptTypeFieldConfigurationValidator :
    AbstractValidator<PatchReceiptTypeFieldConfigurationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchReceiptTypeFieldConfigurationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Placement)
            .Must(p => p is null || Enum.IsDefined(typeof(ReceiptConfiguredPlacement), p.Value))
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Placement.Invalid"].Value);
    }
}

public static class PatchReceiptTypeFieldConfigurationPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/exists"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/isRequired"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/placement"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(
        JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto> patchDocument
    )
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
