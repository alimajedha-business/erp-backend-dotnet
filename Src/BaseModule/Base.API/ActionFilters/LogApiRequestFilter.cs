using System.Diagnostics;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using NGErp.Base.Service.Services;

namespace NGErp.Base.API.ActionFilters;

public sealed class LogApiRequestFilter(
    ILogger<LogApiRequestFilter> logger,
    ICurrentUserService currentUserService
) : IAsyncActionFilter
{
    private readonly ILogger<LogApiRequestFilter> _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var sw = Stopwatch.StartNew();

        var user = _currentUserService.Username ?? "Anonymous";
        var action = context.ActionDescriptor.DisplayName ?? "UnknownAction";
        var endpoint = context.HttpContext.Request.Path.Value ?? "";

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["Endpoint"] = endpoint,
            ["Action"] = action,
            ["User"] = user,
            ["TraceId"] = context.HttpContext.TraceIdentifier
        }))
        {
            var controller = context.RouteData.Values["controller"];
            _logger.LogInformation("Executing {Controller}.{Action}", controller, action);

            foreach (var (name, value) in context.ActionArguments)
            {
                _logger.LogInformation(
                    "Argument {Name} = {@Value}",
                    name,
                    value
                );
            }

            ActionExecutedContext? executed = null;
            try
            {
                executed = await next();
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "Request failed after {ElapsedMs} ms.", sw.ElapsedMilliseconds);
                throw; // keep normal exception handling behavior
            }

            sw.Stop();

            var statusCode = executed.Result switch
            {
                Microsoft.AspNetCore.Mvc.ObjectResult o => o.StatusCode ?? 200,
                Microsoft.AspNetCore.Mvc.StatusCodeResult s => s.StatusCode,
                _ => 200
            };

            _logger.LogInformation(
                "Finished request with {StatusCode} in {ElapsedMs} ms.",
                statusCode,
                sw.ElapsedMilliseconds
            );
        }
    }
}
