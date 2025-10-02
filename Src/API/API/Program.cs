using API.Extentions;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Accounting.Infrastructure.DataAccess;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Morcatko.AspNetCore.JsonMergePatch;
using Common.Infrastructure.Logging;
using General.Infrastructure.DataAccess;
using Warehouse.Infrastructure.DataAccess;
using Accounting.Application.Mappings;
using Common.Application.Mappings;
using Microsoft.EntityFrameworkCore;


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
    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.AddModuleApplications();
    builder.Services.AddInfrastructures(builder.Configuration);
    builder.Services.AddAutoMapper(typeof(AccountingMappingProfile).Assembly);
    //builder.Services.AddAutoMapper(typeof(WarehouseMappingProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(CommonMappingProfile).Assembly);
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    builder.Services.AddControllers(config =>
    {
        config.ReturnHttpNotAcceptable = true;
    }).AddApplicationPart(typeof(Accounting.Presentation.AssemblyReference).Assembly)
    .AddApplicationPart(typeof(General.Presentation.AssemblyReference).Assembly)
    .AddApplicationPart(typeof(Warehouse.Presentation.AssemblyReference).Assembly);
    builder.Services.AddCustomLogging();
    builder.Services.AddControllers().AddSystemTextJsonMergePatch();
    builder.Host.UseSerilog();
    builder.Services.ConfigureSwagger();

    

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.ConfigureExceptionHandler();
    if (app.Environment.IsProduction())
        app.UseHsts();

    //app.UseHttpsRedirection();
    
    app.UseStaticFiles();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.All
    });
    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Noavaran ERP API v1");
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