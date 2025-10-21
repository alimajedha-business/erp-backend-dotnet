using Common.Application.Interfaces;
using Common.Application.Services;
using General.Application.Interfaces.Services;
using General.Application.Mappings;
using General.Application.Services;
using General.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGeneralApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IExceptionLocalizer<GeneralResource>, ExceptionLocalizer<GeneralResource>>();
            return services;
        }
    }
}
