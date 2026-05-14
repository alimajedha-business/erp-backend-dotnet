using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Collections;

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

            if (operation.value is not null && !rule.AllowEmpty && IsEmpty(operation.value))
            {
                throw new InvalidPatchDocumentException(
                    $"Patch value for '{operation.path}' cannot be empty.");
            }
        }
    }

    private static bool IsEmpty(object value)
    {
        if (value is string text)
            return string.IsNullOrWhiteSpace(text);

        if (value is IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();

            try
            {
                return !enumerator.MoveNext();
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
        }

        return false;
    }

    public static bool HasProperty<TDto>(
        JsonPatchDocument<TDto> doc,
        string propertyName
    )
        where TDto : class
    {
        var path = "/" + propertyName.ToLowerInvariant();

        return doc.Operations.Any(op =>
            op.path is not null &&
            op.path.Equals(path, StringComparison.InvariantCultureIgnoreCase)
        );
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
