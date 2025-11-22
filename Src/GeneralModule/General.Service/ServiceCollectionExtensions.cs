using Base.Service.Interfaces;
using Base.Service.Services;
using General.Service.Interfaces;
using General.Service.Interfaces.Services;
using General.Service.Mappings;
using General.Service.Resources;
using General.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace General.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneralServices(this IServiceCollection services)
        {
            services.AddScoped<IGeneralServiceManager, GeneralServiceManager>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IExceptionLocalizer<GeneralResource>, ExceptionLocalizer<GeneralResource>>();
            return services;
        }
    }
}
