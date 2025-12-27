using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace NGErp.General.Infrastructure.Services
{
    public class TokenValidationService
    {
        private readonly DjangoApiService _djangoApiService;
        private readonly ILogger<TokenValidationService> _logger;

        public TokenValidationService(DjangoApiService djangoApiService, ILogger<TokenValidationService> logger)
        {
            _djangoApiService = djangoApiService;
            _logger = logger;
        }

        public ClaimsPrincipal? GetUserFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                
                var claims = new List<Claim>();
                
                // Extract standard claims from Django JWT
                foreach (var claim in jwtToken.Claims)
                {
                    claims.Add(claim);
                }

                // Extract common Django JWT claims
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                if (!string.IsNullOrEmpty(userId))
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
                
                if (!string.IsNullOrEmpty(username))
                    claims.Add(new Claim(ClaimTypes.Name, username));
                
                if (!string.IsNullOrEmpty(email))
                    claims.Add(new Claim(ClaimTypes.Email, email));

                var identity = new ClaimsIdentity(claims, "Bearer");
                return new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting user from token");
                return null;
            }
        }

        public async Task<bool> ValidateTokenWithDjango(string token)
        {
            try
            {
                // Optionally verify token with Django API
                var result = await _djangoApiService.GetAsync<object>("/api/auth/verify-token/", token);
                return result != null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Token validation with Django failed");
                return false;
            }
        }

        public Dictionary<string, string> GetUserInfo(ClaimsPrincipal user)
        {
            return new Dictionary<string, string>
            {
                ["UserId"] = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                           user.FindFirst("user_id")?.Value ?? "",
                ["Username"] = user.FindFirst(ClaimTypes.Name)?.Value ?? 
                              user.FindFirst("username")?.Value ?? "",
                ["Email"] = user.FindFirst(ClaimTypes.Email)?.Value ?? 
                           user.FindFirst("email")?.Value ?? ""
            };
        }
    }
}
