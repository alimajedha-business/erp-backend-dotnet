using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Infrastructure.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneralInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}
