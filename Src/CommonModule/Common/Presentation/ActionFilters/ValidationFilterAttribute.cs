using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private static readonly string[] _bodyVerbs = new[] { "POST", "PUT", "PATCH" };
        public ValidationFilterAttribute()
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpMethod = context.HttpContext.Request.Method.ToUpperInvariant();

            if (!_bodyVerbs.Contains(httpMethod))
                return;

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new
                {
                    message = "Validation failed.",
                    errors = context.ModelState
                        .Where(ms => ms.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
