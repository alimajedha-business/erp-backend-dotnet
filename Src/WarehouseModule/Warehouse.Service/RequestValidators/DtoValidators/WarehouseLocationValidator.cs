using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateWarehouseLocationValidator : AbstractValidator<CreateWarehouseLocationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateWarehouseLocationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseLocation.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["WarehouseLocation.Code.Numeric"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseLocation.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["WarehouseLocation.Title.MinLength"].Value)
            .MaximumLength(250)
            .WithMessage(_localizer["WarehouseLocation.Title.MaxLength"].Value);

        RuleFor(p => p.LevelNo)
            .InclusiveBetween(1, 6)
            .WithMessage(_localizer["WarehouseLocation.LevelNo.Range"].Value);

        RuleFor(p => p.Length)
            .GreaterThan(0)
            .When(p => p.Length.HasValue);

        RuleFor(p => p.Width)
            .GreaterThan(0)
            .When(p => p.Width.HasValue);

        RuleFor(p => p.Height)
            .GreaterThan(0)
            .When(p => p.Height.HasValue);

        RuleFor(p => p.MaxMass)
            .GreaterThan(0)
            .When(p => p.MaxMass.HasValue);

        RuleFor(p => p.MaxVolume)
            .GreaterThan(0)
            .When(p => p.MaxVolume.HasValue);

        RuleFor(p => p.PreferredMassUnitId)
            .NotEmpty()
            .When(p => p.MaxMass.HasValue);

        RuleFor(p => p.PreferredLengthUnitId)
            .NotEmpty()
            .When(p => p.Length.HasValue || p.Width.HasValue || p.Height.HasValue);

        RuleFor(p => p.PreferredVolumeUnitId)
            .NotEmpty()
            .When(p => p.MaxVolume.HasValue);
    }
}

public class PatchWarehouseLocationValidator : AbstractValidator<PatchWarehouseLocationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchWarehouseLocationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseLocation.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["WarehouseLocation.Code.Numeric"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseLocation.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["WarehouseLocation.Title.MinLength"].Value)
            .MaximumLength(250)
            .WithMessage(_localizer["WarehouseLocation.Title.MaxLength"].Value)
            .When(p => p.Title is not null);

        RuleFor(p => p.LevelNo)
            .InclusiveBetween(1, 6)
            .WithMessage(_localizer["WarehouseLocation.LevelNo.Range"].Value)
            .When(p => p.LevelNo is not null);

        RuleFor(p => p.Length)
            .GreaterThan(0)
            .When(p => p.Length.HasValue);

        RuleFor(p => p.Width)
            .GreaterThan(0)
            .When(p => p.Width.HasValue);

        RuleFor(p => p.Height)
            .GreaterThan(0)
            .When(p => p.Height.HasValue);

        RuleFor(p => p.MaxMass)
            .GreaterThan(0)
            .When(p => p.MaxMass.HasValue);

        RuleFor(p => p.MaxVolume)
            .GreaterThan(0)
            .When(p => p.MaxVolume.HasValue);

        RuleFor(p => p.PreferredMassUnitId)
            .NotEmpty()
            .When(p => p.MaxMass.HasValue);

        RuleFor(p => p.PreferredLengthUnitId)
            .NotEmpty()
            .When(p => p.Length.HasValue || p.Width.HasValue || p.Height.HasValue);

        RuleFor(p => p.PreferredVolumeUnitId)
            .NotEmpty()
            .When(p => p.MaxVolume.HasValue);
    }
}

public static class PatchWarehouseLocationPolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/levelNo"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/canStoreItem"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/hasNextLevel"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/length"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/width"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/height"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/maxMass"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/maxVolume"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/preferredMassUnitId"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/preferredLengthUnitId"] = PatchFieldRule.OptionalReplaceOrRemove(),
        ["/preferredVolumeUnitId"] = PatchFieldRule.OptionalReplaceOrRemove()
    };

    public static void Validate(JsonPatchDocument<PatchWarehouseLocationDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
