// Ignore Spelling: Localizer

using Base.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Services;
using Base.Service.Resources;
using General.Service.Resources;

namespace API
{
    public static class ExceptionLocalizerFactory
    {
        public static IExceptionLocalizer ResolveForException(IServiceProvider sp, Exception ex)
        {
            var ns = ex.GetType().Namespace ?? string.Empty;

            var mapping = new (string key, Type resource)[]
            {
                ("General", typeof(GeneralResource)),
            };

            var match = mapping.FirstOrDefault(m => ns.Contains(m.key));

            var resourceType = match.resource ?? typeof(BaseResource);
            var serviceType = typeof(IExceptionLocalizer<>).MakeGenericType(resourceType);

            return (IExceptionLocalizer)sp.GetRequiredService(serviceType);
        }
    }
}
