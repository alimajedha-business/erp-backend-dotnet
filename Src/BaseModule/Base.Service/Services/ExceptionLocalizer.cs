using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Resources;

namespace NGErp.Base.Service.Services;

public class ExceptionLocalizer<TEntityResource>(
    IStringLocalizer<BaseResource> commonLocalizer,
    IStringLocalizer<TEntityResource> moduleLocalizer
) : 
    IExceptionLocalizer<TEntityResource> where TEntityResource : class
{
    private readonly IStringLocalizer<BaseResource> _commonLocalizer = commonLocalizer;
    private readonly IStringLocalizer<TEntityResource> _moduleLocalizer = moduleLocalizer;

    public string Localize(Exception ex)
    {
        if (ex is NotFoundException notFound)
            return LocalizeOrFallback(notFound.LocalizationKey, notFound.Arguments);

        if (ex is ForeignKeyViolationException fkViolation)
            return LocalizeOrFallback(fkViolation.LocalizationKey, fkViolation.Arguments);

        if (ex is ForeignKeyConstraintException fkConstraint)
            return LocalizeOrFallback(fkConstraint.LocalizationKey, fkConstraint.Arguments);

        if (ex is CheckConstraintException checkConstraint)
            return LocalizeOrFallback(checkConstraint.LocalizationKey, checkConstraint.Arguments);

        if (ex is DuplicateInsertException duplicateInsert)
            return LocalizeOrFallback(duplicateInsert.LocalizationKey, duplicateInsert.Arguments);

        // Generic fallback
        return _commonLocalizer["GeneralError"];
    }

    private string LocalizeOrFallback(string key, params object[] args)
    {
        // Module resource first
        var module = _moduleLocalizer[key, args];
        if (!module.ResourceNotFound)
            return module.Value;

        // Shared fallback
        var shared = _commonLocalizer[key, args];
        if (!shared.ResourceNotFound)
            return shared.Value;

        // Raw fallback
        return (args is { Length: > 0 })
            ? string.Format(key, args)
            : key;
    }
}
