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
                if (!string.IsNullOrEmpty(authHeader))
                {
                    // Remove any existing Authorization header from proxy request
                    transformContext.ProxyRequest.Headers.Remove("Authorization");
                    
                    // Clean up the header value and ensure proper format
                    var cleanedHeader = authHeader.Trim();
                    
                    // Django expects: "Bearer <token>" - exactly one space
                    if (cleanedHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        // Extract just the token part
                        var token = cleanedHeader.Substring(7).Trim(); // Remove "Bearer " and trim
                        
                        // Re-add with proper formatting
                        if (!string.IsNullOrEmpty(token))
                        {
                            transformContext.ProxyRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                        }
                    }
                    else
                    {
                        // If no "Bearer " prefix, add it
                        transformContext.ProxyRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {cleanedHeader}");
                    }
                }

                await Task.CompletedTask;
            });
        }
    }
}
