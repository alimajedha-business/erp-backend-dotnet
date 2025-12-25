using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGErp.General.Infrastructure.Services;

namespace NGErp.PythonIntegration.Tests.Fixtures
{
    public class PythonApiTestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; }
        public IConfiguration Configuration { get; }

        public PythonApiTestFixture()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", optional: false)
                .Build();

            var services = new ServiceCollection();

            // Add logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            // Add HTTP client
            var djangoApiBaseUrl = Configuration["DjangoApi:BaseUrl"] ?? "http://localhost:8000";
            services.AddHttpClient<DjangoApiService>(client =>
            {
                client.BaseAddress = new Uri(djangoApiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Add services
            services.AddScoped<IPythonIntegrationService, PythonIntegrationService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
