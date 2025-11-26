using NGErp.Base.Service.Interfaces;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Interfaces;
using NGErp.General.Service.Interfaces.Services;
using NGErp.General.Service.Mappings;
using NGErp.General.Service.Resources;
using NGErp.General.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service
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
