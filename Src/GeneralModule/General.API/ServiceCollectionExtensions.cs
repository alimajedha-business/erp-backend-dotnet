using Microsoft.Extensions.DependencyInjection;

using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneralApiServices(this IServiceCollection services)
        {                
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyUnitRepository, CompanyUnitRepository>();

            return services;
        }
    }
}
