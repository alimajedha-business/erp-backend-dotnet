using Microsoft.AspNetCore.Mvc;
using ERP.API.Services;
using Microsoft.Extensions.Logging;

namespace NGErp.General.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ICurrentUserService currentUserService, ILogger<AuthController> logger)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            if (!_currentUserService.IsAuthenticated)
            {
                return Unauthorized(new { message = "Not authenticated" });
            }

            var userInfo = new
            {
                userId = _currentUserService.UserId,
                username = _currentUserService.Username,
                email = _currentUserService.Email,
                isAuthenticated = _currentUserService.IsAuthenticated
            };

            _logger.LogInformation("User info requested: {Username}", _currentUserService.Username);
            
            return Ok(userInfo);
        }

        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            if (!_currentUserService.IsAuthenticated)
            {
                return Unauthorized(new { valid = false, message = "Invalid or missing token" });
            }

            return Ok(new 
            { 
                valid = true, 
                userId = _currentUserService.UserId,
                username = _currentUserService.Username
            });
        }
    }
}
