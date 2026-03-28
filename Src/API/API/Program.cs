using Asp.Versioning.Routing;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using NGErp.API.Extensions;
using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Infrastructure.Logging;
using NGErp.Base.Service;
using NGErp.Base.Service.Services;

using Prometheus;

using Serilog;

using Swashbuckle.AspNetCore.SwaggerUI;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/ngerp-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.Configure<RouteOptions>(options =>
    {
        options.ConstraintMap.Add("apiVersion", typeof(ApiVersionRouteConstraint));
    });
    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.ConfigureLocalization();    
    builder.Services.AddServices();    
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApiGateway(builder.Configuration);
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    builder.Services.ConfigureControllers();
    builder.Services.AddCustomLogging();    
    builder.Host.UseSerilog();
    builder.Services.ConfigureSwagger();
    builder.Services.AddApiVersioning();

    builder.Services.AddSingleton<IFilterSchemaProvider, FilterSchemaProvider>();
    builder.Services.AddScoped<IAdvancedFilterBuilder, AdvancedFilterBuilder>();

    // Add Prometheus metrics
    builder.Services.AddSingleton<IMetricFactory>(Metrics.DefaultFactory);

    var app = builder.Build();

    var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(locOptions.Value);
    app.UseSerilogRequestLogging();
    app.ConfigureExceptionHandler();
    if (app.Environment.IsProduction())
        app.UseHsts();

    //app.UseHttpsRedirection();

    app.UseForwardedHeaders();
    app.UseStaticFiles();

    //app.UseForwardedHeaders(new ForwardedHeadersOptions
    //{
    //    ForwardedHeaders = ForwardedHeaders.All
    //});

    app.UseCors("CorsPolicy");
    app.UseRouting();
    
    // Add Prometheus metrics endpoint
    app.UseHttpMetrics();
    
    app.UseApiGateway();
    //app.UseAuthentication();
    app.UseAuthorization();  
    app.MapControllers();
    app.MapReverseProxy();
    
    // Map Prometheus metrics endpoint
    app.MapMetrics();

    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        foreach (var module in ProjectConstants.Modules)
        {
            var lower = module.ToLower();
            s.SwaggerEndpoint($"/swagger/v1-{lower}/swagger.json", $"{module} API");
        }
        s.RoutePrefix = "doc/v1";
        s.DocExpansion(DocExpansion.None); // Ensure all endpoints and tags are collapsed
        s.ConfigObject.AdditionalItems["syntaxHighlight"] = false;
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}