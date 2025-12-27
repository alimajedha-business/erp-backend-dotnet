using NGErp.API.Middleware;
using NGErp.API.Transforms;

namespace NGErp.API.Extensions
{
    public static class GatewayExtensions
    {
        public static IServiceCollection AddApiGateway(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"))
                .AddTransforms<DjangoHeaderTransform>();

            return services;
        }

        public static IApplicationBuilder UseApiGateway(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtAuthenticationMiddleware>();
            app.UseMiddleware<UserInfoExtractionMiddleware>();
            app.UseMiddleware<PythonApiMetricsMiddleware>();

            return app;
        }
    }
}
