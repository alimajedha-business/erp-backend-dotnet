using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

using NGErp.Base.Domain.Exceptions;

namespace NGErp.Base.Service.Validators;

public static class PatchPolicyValidator
{
    public static void Validate<TDto>(
        JsonPatchDocument<TDto>? patchDocument,
        IReadOnlyDictionary<string, PatchFieldRule> rules
    )
        where TDto : class
    {
        if (patchDocument is null || patchDocument.Operations.Count == 0)
            throw new InvalidPatchDocumentException("Patch document is required.");

        foreach (var operation in patchDocument.Operations)
        {
            if (!rules.TryGetValue(operation.path, out var rule))
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

            if (
                operation.value is string text &&
                string.IsNullOrWhiteSpace(text) &&
                !rule.AllowEmpty
            )
            {
                throw new InvalidPatchDocumentException(
                    $"Patch value for '{operation.path}' cannot be empty.");
            }
        }
    }
}

public sealed record PatchFieldRule(
    bool AllowNull,
    bool AllowEmpty,
    bool AllowRemove,
    HashSet<OperationType> AllowedOperations
)
{
    public static PatchFieldRule RequiredReplaceOnly() =>
        new(false, false, false, [OperationType.Replace]);

    public static PatchFieldRule OptionalReplaceOrRemove() =>
        new(true, true, true, [OperationType.Replace, OperationType.Remove]);
}