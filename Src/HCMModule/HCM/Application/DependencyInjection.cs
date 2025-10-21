using Common.Application.Interfaces;
using Common.Application.Services;
using General.Resources;
using HCM.Application.Interfaces.Services;
using HCM.Application.Mappings;
using HCM.Application.Services;
using HCM.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHCMApplication(this IServiceCollection services)
        {
            services.AddScoped<IHCMServiceManager, HCMServiceManager>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IExceptionLocalizer<HCMResource>, ExceptionLocalizer<HCMResource>>();
            return services;
        }
    }
}
