using System.Security.Claims;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace NGErp.API.Transforms
{
    public class DjangoHeaderTransform : ITransformProvider
    {
        public void ValidateRoute(TransformRouteValidationContext context)
        {
        }

        public void ValidateCluster(TransformClusterValidationContext context)
        {
        }

        public void Apply(TransformBuilderContext context)
        {
            context.AddRequestTransform(async transformContext =>
            {
                var httpContext = transformContext.HttpContext;
                
                // Extract and forward user information headers
                if (httpContext.User?.Identity?.IsAuthenticated == true)
                {
                    var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                httpContext.User.FindFirst("user_id")?.Value;
                    var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? 
                                  httpContext.User.FindFirst("username")?.Value;
                    var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? 
                               httpContext.User.FindFirst("email")?.Value;

                    if (!string.IsNullOrEmpty(userId))
                        transformContext.ProxyRequest.Headers.Add("X-User-Id", userId);
                    
                    if (!string.IsNullOrEmpty(username))
                        transformContext.ProxyRequest.Headers.Add("X-Username", username);
                    
                    if (!string.IsNullOrEmpty(email))
                        transformContext.ProxyRequest.Headers.Add("X-User-Email", email);
                }

                // Forward Authorization header properly
                // YARP automatically copies most headers, but we need to ensure Authorization is correct
                var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
                var token = string.Empty;

                if (!string.IsNullOrEmpty(authHeader))
                {
                    // Clean up the header value and ensure proper format
                    var cleanedHeader = authHeader.Trim();
                    if (cleanedHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        token = cleanedHeader.Substring(7).Trim();
                    }
                    else
                    {
                        token = cleanedHeader;
                    }
                }
                else
                {
                    // Try to get from cookies if header is missing
                    var cookieNames = new[] { "access", "access_token", "jwt_token", "token", "auth_token", "refresh", "sessionid" };
                    foreach (var cookieName in cookieNames)
                    {
                        if (httpContext.Request.Cookies.TryGetValue(cookieName, out var cookieToken) && !string.IsNullOrEmpty(cookieToken))
                        {
                            token = cookieToken;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(token))
                {
                    // Remove any existing Authorization header from proxy request
                    transformContext.ProxyRequest.Headers.Remove("Authorization");
                    
                    // Django expects: "Bearer <token>" - exactly one space
                    transformContext.ProxyRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                }

                await Task.CompletedTask;
            });
        }
    }
}
