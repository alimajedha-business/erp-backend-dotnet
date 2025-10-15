// Ignore Spelling: Middleware

using Common.Domain.Exceptions;
using Common.ErrorModel;
using Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Reflection;
using API.Localization;

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
                    //var localizer = context.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();
                    //var localizer = context.RequestServices.GetRequiredService<IStringLocalizer>();
                    var localizerFactory = context.RequestServices.GetRequiredService<IStringLocalizerFactory>();

                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        //var localizedMessage = localizer[contextFeature.Error.Message];
                        // Create localizer for shared resources
                        var localizer = localizerFactory.Create("SharedResource", Assembly.GetExecutingAssembly().GetName().Name!);

                        string message;

                        if (contextFeature.Error is NotFoundException notFoundEx)
                        {
                            // Use the localization key with arguments
                            var localized = localizer[notFoundEx.LocalizationKey, notFoundEx.Arguments];
                            message = localized.Value;
                        }
                        else
                        {
                            // For other exceptions, try to localize the message
                            var localized = localizer[contextFeature.Error.Message];
                            message = localized.Value;
                        }

                        logger.LogError(null, $"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}
