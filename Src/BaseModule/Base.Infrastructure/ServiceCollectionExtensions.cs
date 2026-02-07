using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.Services;
using NGErp.Base.Service.Services;

namespace NGErp.Base.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NGERPDATABASE")));
            var djangoApiBaseUrl = configuration.GetSection("DjangoApi:BaseUrl").Value ?? "http://localhost:8000";
            services.AddHttpClient<DjangoApiService>(client =>
            {
                client.BaseAddress = new Uri(djangoApiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddScoped<TokenValidationService>();
            return services;
        }
    }
}
