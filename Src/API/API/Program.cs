using Accounting.Application.Mappings;
using Accounting.Infrastructure.DataAccess;
using API.Extensions;
using Asp.Versioning.Routing;
using Common.Application;
using Common.Application.Mappings;
using Common.Application.Services;
using Common.Infrastructure.Logging;
using Common.Presentation.ActionFilters;
using General.Infrastructure.DataAccess;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Morcatko.AspNetCore.JsonMergePatch;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using Warehouse.Infrastructure.DataAccess;


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
    builder.Services.AddModuleApplications();
    builder.Services.AddScoped<ValidationFilterAttribute>();
    builder.Services.AddInfrastructures(builder.Configuration);    
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    builder.Services.ConfigureControllers();
    builder.Services.AddCustomLogging();    
    builder.Host.UseSerilog();
    builder.Services.ConfigureSwagger();
    builder.Services.AddApiVersioning();

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
    //app.UseAuthentication();
    app.UseAuthorization();  
    app.MapControllers();

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