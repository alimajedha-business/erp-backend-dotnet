// Ignore Spelling: Localizer

using NGErp.Base.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGErp.Base.Service.Resources;
using NGErp.General.Service.Resources;

namespace NGErp.API.Services
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
