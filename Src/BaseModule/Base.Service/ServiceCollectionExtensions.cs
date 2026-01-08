using Microsoft.Extensions.DependencyInjection;
using NGErp.Base.Service.Interfaces;
using NGErp.Base.Service.Mappings;
using NGErp.Base.Service.Resources;
using NGErp.Base.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.Base.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IExceptionLocalizer<BaseResource>, ExceptionLocalizer<BaseResource>>();            
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
