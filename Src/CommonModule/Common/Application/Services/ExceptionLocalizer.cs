// Ignore Spelling: Localizer

using Common.Application.Interfaces;
using Common.Domain.Exceptions;
using Common.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Services
{
    public class ExceptionLocalizer<TEntityResource> : IExceptionLocalizer<TEntityResource> where TEntityResource : class
    {
        private readonly IStringLocalizer<CommonResource> _commonLocalizer;
        private readonly IStringLocalizer<TEntityResource> _moduleLocalizer;

        public ExceptionLocalizer(IStringLocalizer<CommonResource> commonLocalizer, IStringLocalizer<TEntityResource> moduleLocalizer)
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
