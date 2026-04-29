using FluentValidation;

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
        if (ex is ValidationException)
            return _commonLocalizer["ValidationFailed"];

        if (ex is NotFoundException notFound)
            return LocalizeOrFallback(notFound.LocalizationKey, notFound.Arguments);

        if (ex is BadRequestException badRequest)
            return LocalizeOrFallback(badRequest.LocalizationKey, badRequest.Arguments);

        if (ex is ForeignKeyConstraintException fkConstraint)
            return LocalizeOrFallback(fkConstraint.LocalizationKey, fkConstraint.Arguments);

        if (ex is CheckConstraintException checkConstraint)
            return LocalizeOrFallback(checkConstraint.LocalizationKey, checkConstraint.Arguments);

        if (ex is DuplicateResourceException duplicateInsert)
            return LocalizeOrFallback(duplicateInsert.LocalizationKey, duplicateInsert.Arguments);

        // Generic fallback
        return _commonLocalizer["GeneralError"];
    }

    public string Localize(string key, params object[] args) => LocalizeOrFallback(key, args);

    private string LocalizeOrFallback(string key, object[] args)
    {
        var localizedArgs = args
            .Select(LocalizeArgument)
            .ToArray();

        var localized = _moduleLocalizer[key, localizedArgs];
        if (!localized.ResourceNotFound)
            return localized.Value;

        localized = _commonLocalizer[key, localizedArgs];
        return localized.ResourceNotFound ? key : localized.Value;
    }

    private object LocalizeArgument(object arg)
    {
        if (arg is not string stringArg)
            return arg;

        var localizedArg = _moduleLocalizer[stringArg];
        if (!localizedArg.ResourceNotFound)
            return localizedArg.Value;

        localizedArg = _commonLocalizer[stringArg];
        return localizedArg.ResourceNotFound ? stringArg : localizedArg.Value;
    }
}
