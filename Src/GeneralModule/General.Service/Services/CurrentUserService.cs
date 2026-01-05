using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NGErp.General.Service.Services
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Username { get; }
        string? Email { get; }
        string? Token { get; }
        bool IsAuthenticated { get; }
        ClaimsPrincipal? User { get; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => 
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value;

        public string? Username => 
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ??
            _httpContextAccessor.HttpContext?.User?.FindFirst("username")?.Value;

        public string? Email => 
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ??
            _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value;

        public string? Token
        {
            get
            {
                var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return authHeader.Substring("Bearer ".Length).Trim();
                }
                return null;
            }
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}
