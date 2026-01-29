using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Resources;

namespace NGErp.Base.Service.Services
{
    public class ExceptionLocalizer<TEntityResource> : IExceptionLocalizer<TEntityResource> where TEntityResource : class
    {
        private readonly IStringLocalizer<BaseResource> _commonLocalizer;
        private readonly IStringLocalizer<TEntityResource> _moduleLocalizer;

        public ExceptionLocalizer(IStringLocalizer<BaseResource> commonLocalizer, IStringLocalizer<TEntityResource> moduleLocalizer)
        {
            _commonLocalizer = commonLocalizer;
            _moduleLocalizer = moduleLocalizer;
        }

        public string Localize(Exception ex)
        {
            if (ex is NotFoundException notFound)
            {
                // Module resource first
                var localized = _moduleLocalizer[notFound.LocalizationKey, notFound.Arguments];
                if (!localized.ResourceNotFound)
                    return localized.Value;

                // Fallback to shared
                var shared = _commonLocalizer[notFound.LocalizationKey, notFound.Arguments];
                if (!shared.ResourceNotFound)
                    return shared.Value;

                // Fallback to raw key
                return string.Format(notFound.LocalizationKey, notFound.Arguments);
            }

            // Generic fallback
            return _commonLocalizer["GeneralError"];
        }
    }
}
