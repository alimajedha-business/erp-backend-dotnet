using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateCategoryValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Category.Code.NotEmpty"].Value)
            .MaximumLength(64)
            .WithMessage(_localizer["Category.Code.MaxLength"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(_localizer["Category.Title.MaxLength"].Value);

        RuleFor(p => p.LevelNo)
            .InclusiveBetween(1, 6)
            .WithMessage(_localizer["Category.LevelNo.Range"].Value);
    }
}

public class PatchCategoryValidator : AbstractValidator<PatchCategoryDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public PatchCategoryValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(localizer["Category.Code.NotEmpty"].Value)
            .MaximumLength(64)
            .WithMessage(localizer["Category.Code.MaxLength"].Value)
            .When(p => p.Code is not null);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(localizer["Category.Title.MinLength"].Value)
            .MaximumLength(200)
            .WithMessage(localizer["Category.Title.MaxLength"].Value)
            .When(p => p.Title is not null);
    }
}

public static class PatchCategoryPolicy
{
    public static void Validate(JsonPatchDocument<PatchCategoryDto> patchDocument)
    {
        if (patchDocument is null || patchDocument.Operations.Count == 0)
            throw new InvalidPatchDocumentException("Patch document is required.");

        foreach (var operation in patchDocument.Operations)
        {
            if (!Rules.TryGetValue(operation.path, out var rule))
                throw new InvalidPatchDocumentException(
                    $"Patch path '{operation.path}' is not allowed.");

            if (!rule.AllowedOperations.Contains(operation.OperationType))
                throw new InvalidPatchDocumentException(
                    $"Patch operation '{operation.op}' is not allowed for '{operation.path}'.");

            if (operation.OperationType == OperationType.Remove)
            {
                if (!rule.AllowRemove)
                    throw new InvalidPatchDocumentException(
                        $"Patch path '{operation.path}' cannot be removed.");

                continue;
            }

            if (operation.value is null && !rule.AllowNull)
                throw new InvalidPatchDocumentException(
                    $"Patch value for '{operation.path}' cannot be null.");

            if (operation.value is string text &&
                string.IsNullOrWhiteSpace(text) &&
                !rule.AllowEmpty)
            {
                throw new InvalidPatchDocumentException(
                    $"Patch value for '{operation.path}' cannot be empty.");
            }
        }
    }

    private static readonly Dictionary<string, PatchFieldRule> Rules =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["/code"] = new(
                AllowNull: false,
                AllowEmpty: false,
                AllowRemove: false,
                AllowedOperations: [OperationType.Replace]
            ),

            ["/title"] = new(
                AllowNull: false,
                AllowEmpty: false,
                AllowRemove: false,
                AllowedOperations: [OperationType.Replace]
            ),

            ["/hasNextLevel"] = new(
                AllowNull: false,
                AllowEmpty: false,
                AllowRemove: false,
                AllowedOperations: [OperationType.Replace]
            )
    };

    private sealed record PatchFieldRule(
        bool AllowNull,
        bool AllowEmpty,
        bool AllowRemove,
        HashSet<OperationType> AllowedOperations
    );
}