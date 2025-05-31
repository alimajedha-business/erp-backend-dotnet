using API.Extentions;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Common.Logging;
using Accounting.Infrastructure.DataAccess;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Morcatko.AspNetCore.JsonMergePatch;


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
    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureServiceManager();
    builder.Services.ConfigureSqlContext(builder.Configuration);
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    builder.Services.AddControllers(config =>
    {
        config.ReturnHttpNotAcceptable = true;
    }).AddApplicationPart(typeof(Accounting.Presentation.AssemblyReference).Assembly);
    builder.Services.AddCustomLogging();
    builder.Services.AddControllers().AddSystemTextJsonMergePatch();
    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.ConfigureExceptionHandler();
    if (app.Environment.IsProduction())
        app.UseHsts();

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.All
    });
    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    app.MapControllers();

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