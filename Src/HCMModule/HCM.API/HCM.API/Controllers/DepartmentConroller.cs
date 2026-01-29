using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGErp.HCM.Service.Services;
using NGErp.Base.Service.RequestFeatures;

namespace NGErp.HCM.API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-hcm")]
    [Route("api/v{version:apiVersion}/hcm/Departments")]
    [JwtAuthorize] // Require authentication for all endpoints
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, ICurrentUserService currentUserService)
        {
            _departmentService = departmentService;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] RequestParameters requestParameters)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Endpoint"] = "GetDepartments",
                ["User"] = _currentUserService.Username ?? "Anonymous"
            }))
            {
                try
                {
                    _logger.LogInformation("Fetching all Departments for user {Username}", _currentUserService.Username);

                    var departments = await _departmentService.GetDepartmentsAsync(requestParameters);

                    _logger.LogInformation("Retrieved {Count} Departments", departments.Count);

                    return Ok(new
                    {
                        success = true,
                        data = departments,
                        count = departments.Count
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching Departments for user {Username}",
                        _currentUserService.Username);
                    return StatusCode(500, new
                    {
                        success = false,
                        error = "Failed to fetch Departments",
                        message = ex.Message
                    });
                }
            }
        }
    }
}
