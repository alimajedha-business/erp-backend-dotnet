using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateAttributeValidator : AbstractValidator<CreateAttributeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateAttributeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Attribute.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["Attribute.Code.Numeric"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Attribute.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Attribute.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["Attribute.Title.MaxLength"].Value);

        RuleFor(p => p.DataType)
            .IsInEnum()
            .WithMessage(_localizer["Attribute.DataType.Invalid"].Value);

        RuleFor(p => p.AttributeEntity)
            .IsInEnum()
            .WithMessage(_localizer["Attribute.AttributeEntity.Invalid"].Value);
    }
}

public class PatchAttributeValidator : AbstractValidator<PatchAttributeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchAttributeValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Attribute.Code.NotEmpty"].Value)
            .GreaterThan(0)
            .WithMessage(_localizer["Attribute.Code.Numeric"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Attribute.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Attribute.Title.MinLength"].Value)
            .MaximumLength(50)
            .WithMessage(_localizer["Attribute.Title.MaxLength"].Value)
            .When(p => p.Title is not null);

        RuleFor(p => p.DataType)
            .Must(p => p is null || Enum.IsDefined(typeof(AttributeDataType), p.Value))
            .WithMessage(_localizer["Attribute.DataType.Invalid"].Value);

        RuleFor(p => p.AttributeEntity)
            .Must(p => p is null || Enum.IsDefined(typeof(AttributeEntity), p.Value))
            .WithMessage(_localizer["Attribute.AttributeEntity.Invalid"].Value);
    }
}

public static class PatchAttributePolicy
{
    private static readonly Dictionary<string, PatchFieldRule> Rules =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["/code"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/title"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/dataType"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/attributeEntity"] = PatchFieldRule.RequiredReplaceOnly(),
        ["/isRequired"] = PatchFieldRule.RequiredReplaceOnly()
    };

    public static void Validate(JsonPatchDocument<PatchAttributeDto> patchDocument)
    {
        PatchPolicyValidator.Validate(patchDocument, Rules);
    }
}
