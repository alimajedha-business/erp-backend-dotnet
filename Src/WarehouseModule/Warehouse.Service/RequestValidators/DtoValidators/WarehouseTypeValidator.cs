using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateWarehouseTypeValidator : AbstractValidator<CreateWarehouseTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateWarehouseTypeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseType.Code.NotEmpty"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseType.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["WarehouseType.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["WarehouseType.Title.MaxLength"].Value);
    }
}

public class PatchWarehouseTypeValidator : AbstractValidator<PatchWarehouseTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchWarehouseTypeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseType.Code.NotEmpty"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseType.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["WarehouseType.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["WarehouseType.Title.MaxLength"].Value)
            .When(p => p.Title is not null);
    }
}

public static class PatchWarehouseTypePolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchWarehouseTypeDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
