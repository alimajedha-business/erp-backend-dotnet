using System.Security.Claims;

namespace NGErp.API.Middleware
{
    public class UserInfoExtractionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserInfoExtractionMiddleware> _logger;

        public UserInfoExtractionMiddleware(
            RequestDelegate next,
            ILogger<UserInfoExtractionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                            context.User.FindFirst("user_id")?.Value;
                var username = context.User.FindFirst(ClaimTypes.Name)?.Value ?? 
                              context.User.FindFirst("username")?.Value;
                var email = context.User.FindFirst(ClaimTypes.Email)?.Value ?? 
                           context.User.FindFirst("email")?.Value;

                if (!string.IsNullOrEmpty(userId))
                    context.Items["UserId"] = userId;
                
                if (!string.IsNullOrEmpty(username))
                    context.Items["Username"] = username;
                
                if (!string.IsNullOrEmpty(email))
                    context.Items["Email"] = email;

                _logger.LogDebug("User info extracted - UserId: {UserId}, Username: {Username}", 
                    userId, username);
            }

            await _next(context);
        }
    }
}
