using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace Common.Infrastructure.Logging
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomLogging(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddSingleton(Log.Logger); // Serilog's static ILogger instance
            return services;
        }
    }
}