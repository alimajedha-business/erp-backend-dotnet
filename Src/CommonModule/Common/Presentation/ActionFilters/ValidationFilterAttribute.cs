using Common.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Common.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private static readonly string[] _bodyVerbs = new[] { "POST", "PUT", "PATCH" };
        private readonly IStringLocalizer _localizer;

        public ValidationFilterAttribute(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(typeof(CommonResource));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpMethod = context.HttpContext.Request.Method.ToUpperInvariant();

            if (!_bodyVerbs.Contains(httpMethod))
                return;
            
            if (!context.ModelState.IsValid)
            {
                // Replace known system errors with localized equivalents
                foreach (var entry in context.ModelState)
                {
                    var errors = entry.Value?.Errors;
                    if (errors == null || errors.Count == 0)
                        continue;

                    for (int i = 0; i < errors.Count; i++)
                    {
                        var errorMessage = errors[i].ErrorMessage;

                        // Normalize and match known framework messages
                        if (errorMessage.Contains("A non-empty request body is required.", StringComparison.OrdinalIgnoreCase))
                            errorMessage = _localizer["ObjectIsNull"]; 

                        else if (errorMessage.Contains("The", StringComparison.OrdinalIgnoreCase) &&
                                 errorMessage.Contains("field is required.", StringComparison.OrdinalIgnoreCase))
                            errorMessage = _localizer["Required"];

                        if (errorMessage.Contains("could not be converted", StringComparison.OrdinalIgnoreCase))
                            errorMessage = _localizer["InvalidValue"];

                        else if (errorMessage.Contains("Unexpected character", StringComparison.OrdinalIgnoreCase))
                            errorMessage = _localizer["InvalidJsonFormat"];

                        // Replace the error text
                        errors[i] = new ModelError(errorMessage);
                    }
                }

                context.Result = new UnprocessableEntityObjectResult(new
                {
                    message = (string)_localizer["ValidationFailed"], 
                    errors = context.ModelState
                        .Where(ms => ms.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                });

                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
