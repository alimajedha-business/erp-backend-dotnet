using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Service.Mappings;
using NGErp.Base.Service.Resources;
using NGErp.Base.Service.Services;

namespace NGErp.Base.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IExcelExportService, ExcelExportService<BaseResource>>();
            services.AddScoped<IExceptionLocalizer<BaseResource>, ExceptionLocalizer<BaseResource>>();

            return services;
        }
    }
}
