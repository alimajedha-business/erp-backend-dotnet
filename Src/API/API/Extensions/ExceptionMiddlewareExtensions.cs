// Ignore Spelling: Middleware

using Common.Application.Interfaces;
using Common.Domain.Exceptions;
using Common.ErrorModel;
using Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Reflection;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(static appError =>
            {
                appError.Run(async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILoggerService>();
                    var exceptionLocalizer = context.RequestServices.GetRequiredService<IExceptionLocalizer>();

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var error = contextFeature.Error;

                        context.Response.StatusCode = error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        string localizedMessage = exceptionLocalizer.Localize(error);

                        logger.LogError(null, $"Something went wrong: {error}");

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = localizedMessage
                        }.ToString());
                    }
                });
            });
        }
    }
}
