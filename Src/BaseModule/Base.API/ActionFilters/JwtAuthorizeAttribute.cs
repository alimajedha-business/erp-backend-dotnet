using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NGErp.Base.API.ActionFilters
{
    /// <summary>
    /// Custom authorization filter that validates JWT token similar to Django
    /// Returns 401 Unauthorized if token is missing or invalid
    /// </summary>
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Check if user is authenticated (token was validated successfully)
            if (user?.Identity?.IsAuthenticated != true)
            {
                // Return 401 Unauthorized like Django does
                context.Result = new UnauthorizedObjectResult(new
                {
                    detail = "Authentication credentials were not provided or are invalid.",
                    code = "not_authenticated"
                });
                return;
            }

            // Additional validation: Check if token has required claims
            var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ??
                        user.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    detail = "Invalid token: missing user_id claim.",
                    code = "invalid_token"
                });
                return;
            }
        }
    }
}
