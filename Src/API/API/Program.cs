using API.Extentions;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Common.Logging;
using Accounting.Infrastructure.DataAccess;

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

    builder.Services.AddControllers();
    builder.Services.AddCustomLogging();
    builder.Host.UseSerilog();

    builder.Services.AddDbContext<AccountingDbContext>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
    else
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