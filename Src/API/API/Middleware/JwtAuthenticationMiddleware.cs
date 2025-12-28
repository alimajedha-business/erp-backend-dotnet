using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NGErp.API.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtAuthenticationMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationMiddleware(
            RequestDelegate next,
            ILogger<JwtAuthenticationMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Try to get token from Authorization header first
            var token = ExtractTokenFromHeader(context);
            
            // If not found, try to get from cookie
            if (string.IsNullOrEmpty(token))
            {
                token = ExtractTokenFromCookie(context);
            }

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var principal = ValidateToken(token);
                    if (principal != null)
                    {
                        context.User = principal;
                        _logger.LogDebug("JWT token validated successfully for user: {Username}", 
                            principal.FindFirst(ClaimTypes.Name)?.Value ?? 
                            principal.FindFirst("username")?.Value ?? "Unknown");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "JWT token validation failed");
                }
            }
            else
            {
                _logger.LogDebug("No JWT token found in header or cookie for request: {Path}", context.Request.Path);
            }

            await _next(context);
        }

        private string? ExtractTokenFromHeader(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }

        private string? ExtractTokenFromCookie(HttpContext context)
        {
            // Try common cookie names
            var cookieNames = new[] { "access_token", "jwt_token", "token", "auth_token", "sessionid" };
            
            foreach (var cookieName in cookieNames)
            {
                if (context.Request.Cookies.TryGetValue(cookieName, out var token))
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        _logger.LogDebug("Found token in cookie: {CookieName}", cookieName);
                        return token;
                    }
                }
            }
            
            return null;
        }

        private ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];
                var validateIssuer = jwtSettings.GetValue<bool>("ValidateIssuer", false);
                var validateAudience = jwtSettings.GetValue<bool>("ValidateAudience", false);
                var validateLifetime = jwtSettings.GetValue<bool>("ValidateLifetime", true);

                var handler = new JwtSecurityTokenHandler();
                
                // Always validate token expiration
                if (string.IsNullOrEmpty(secretKey))
                {
                    // Even without secret key, we should validate expiration like Django does
                    var jwtToken = handler.ReadJwtToken(token);
                    
                    // Check if token is expired
                    if (validateLifetime && jwtToken.ValidTo < DateTime.UtcNow)
                    {
                        _logger.LogWarning("Token has expired. Valid until: {ValidTo}, Current time: {Now}", 
                            jwtToken.ValidTo, DateTime.UtcNow);
                        return null;
                    }
                    
                    var claims = jwtToken.Claims.ToList();

                    var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
                    var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
                    var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                    if (!string.IsNullOrEmpty(userId) && !claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

                    if (!string.IsNullOrEmpty(username) && !claims.Any(c => c.Type == ClaimTypes.Name))
                        claims.Add(new Claim(ClaimTypes.Name, username));

                    if (!string.IsNullOrEmpty(email) && !claims.Any(c => c.Type == ClaimTypes.Email))
                        claims.Add(new Claim(ClaimTypes.Email, email));

                    var identity = new ClaimsIdentity(claims, "Bearer");
                    return new ClaimsPrincipal(identity);
                }
                else
                {
                    var key = Encoding.UTF8.GetBytes(secretKey);
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = validateIssuer,
                        ValidateAudience = validateAudience,
                        ValidateLifetime = validateLifetime,
                        ClockSkew = TimeSpan.Zero
                    };

                    if (validateIssuer)
                    {
                        validationParameters.ValidIssuer = jwtSettings["Issuer"];
                    }

                    if (validateAudience)
                    {
                        validationParameters.ValidAudience = jwtSettings["Audience"];
                    }

                    var principal = handler.ValidateToken(token, validationParameters, out _);
                    
                    var claims = principal.Claims.ToList();
                    var userId = principal.FindFirst("user_id")?.Value;
                    var username = principal.FindFirst("username")?.Value;
                    var email = principal.FindFirst("email")?.Value;

                    if (!string.IsNullOrEmpty(userId) && !claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

                    if (!string.IsNullOrEmpty(username) && !claims.Any(c => c.Type == ClaimTypes.Name))
                        claims.Add(new Claim(ClaimTypes.Name, username));

                    if (!string.IsNullOrEmpty(email) && !claims.Any(c => c.Type == ClaimTypes.Email))
                        claims.Add(new Claim(ClaimTypes.Email, email));

                    var identity = new ClaimsIdentity(claims, "Bearer");
                    return new ClaimsPrincipal(identity);
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning("Token has expired: {Message}", ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token validation error");
                return null;
            }
        }
    }
}
