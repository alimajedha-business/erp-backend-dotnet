// Ignore Spelling: Localizer

using Common.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.Domain.Exceptions;
using Common.Application.Services;
using HCM.Resources;
using Common.Resources;
using Accounting.Resources;
using General.Resources;

namespace API
{
    public static class ExceptionLocalizerFactory
    {
        public static IExceptionLocalizer ResolveForException(IServiceProvider sp, Exception ex)
        {
            var ns = ex.GetType().Namespace ?? string.Empty;

            var mapping = new (string key, Type resource)[]
            {
                ("Accounting", typeof(AccountingResource)),
                ("HCM", typeof(HCMResource)),
                ("General", typeof(GeneralResource)),
            };

            var match = mapping.FirstOrDefault(m => ns.Contains(m.key));

            var resourceType = match.resource ?? typeof(CommonResource);
            var serviceType = typeof(IExceptionLocalizer<>).MakeGenericType(resourceType);

            return (IExceptionLocalizer)sp.GetRequiredService(serviceType);
        }
    }
}
