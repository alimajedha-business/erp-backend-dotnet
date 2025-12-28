using Microsoft.AspNetCore.Mvc;
using NGErp.General.Infrastructure.Services;
using NGErp.General.Service.DTOs.PythonApi;
using ERP.API.Services;
using Microsoft.Extensions.Logging;
using NGErp.Base.API.ActionFilters;

namespace NGErp.General.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtAuthorize] // Require authentication for all endpoints
    public class PythonCompaniesController : ControllerBase
    {
        private readonly IPythonIntegrationService _pythonIntegrationService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<PythonCompaniesController> _logger;

        public PythonCompaniesController(
            IPythonIntegrationService pythonIntegrationService,
            ICurrentUserService currentUserService,
            ILogger<PythonCompaniesController> logger)
        {
            _pythonIntegrationService = pythonIntegrationService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        /// <summary>
        /// Get all companies from Django/Python API
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Endpoint"] = "GetCompanies",
                ["User"] = _currentUserService.Username ?? "Anonymous"
            }))
            {
                try
                {
                    _logger.LogInformation("Fetching all companies from Django API for user {Username}", 
                        _currentUserService.Username);
                    
                    var companies = await _pythonIntegrationService.GetCompaniesAsync(
                        _currentUserService.Token
                    );

                    _logger.LogInformation("Retrieved {Count} companies from Django API", companies.Count);
                    
                    return Ok(new
                    {
                        success = true,
                        data = companies,
                        count = companies.Count
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching companies from Django API for user {Username}", 
                        _currentUserService.Username);
                    return StatusCode(500, new
                    {
                        success = false,
                        error = "Failed to fetch companies from Python API",
                        message = ex.Message
                    });
                }
            }
        }

        /// <summary>
        /// Get a specific company by ID from Django/Python API
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _pythonIntegrationService.GetCompanyByIdAsync(
                    id,
                    _currentUserService.Token
                );

                if (company == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"Company with ID {id} not found in Python API"
                    });
                }

                _logger.LogInformation("Retrieved company {Id} from Django API", id);
                
                return Ok(new
                {
                    success = true,
                    data = company
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching company {Id} from Django API", id);
                return StatusCode(500, new
                {
                    success = false,
                    error = $"Failed to fetch company {id} from Python API",
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new company in Django/Python API
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto)
        {
            try
            {
                if (!_currentUserService.IsAuthenticated)
                {
                    return Unauthorized(new { success = false, error = "User not authenticated" });
                }

                var company = await _pythonIntegrationService.CreateCompanyAsync(
                    dto,
                    _currentUserService.Token
                );

                _logger.LogInformation("Created company {Name} in Django API by user {Username}",
                    company.Name,
                    _currentUserService.Username);

                return CreatedAtAction(
                    nameof(GetCompany),
                    new { id = company.Id },
                    new
                    {
                        success = true,
                        data = company,
                        message = "Company created successfully in Python API"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company in Django API");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Failed to create company in Python API",
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Update a company in Django/Python API
        /// </summary>
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDto dto)
        {
            try
            {
                if (!_currentUserService.IsAuthenticated)
                {
                    return Unauthorized(new { success = false, error = "User not authenticated" });
                }

                var company = await _pythonIntegrationService.UpdateCompanyAsync(
                    id,
                    dto,
                    _currentUserService.Token
                );

                _logger.LogInformation("Updated company {Id} in Django API by user {Username}",
                    id,
                    _currentUserService.Username);

                return Ok(new
                {
                    success = true,
                    data = company,
                    message = "Company updated successfully in Python API"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company {Id} in Django API", id);
                return StatusCode(500, new
                {
                    success = false,
                    error = $"Failed to update company {id} in Python API",
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a company from Django/Python API
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                if (!_currentUserService.IsAuthenticated)
                {
                    return Unauthorized(new { success = false, error = "User not authenticated" });
                }

                var success = await _pythonIntegrationService.DeleteCompanyAsync(
                    id,
                    _currentUserService.Token
                );

                if (!success)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"Company with ID {id} not found or could not be deleted"
                    });
                }

                _logger.LogInformation("Deleted company {Id} from Django API by user {Username}",
                    id,
                    _currentUserService.Username);

                return Ok(new
                {
                    success = true,
                    message = $"Company {id} deleted successfully from Python API"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company {Id} from Django API", id);
                return StatusCode(500, new
                {
                    success = false,
                    error = $"Failed to delete company {id} from Python API",
                    message = ex.Message
                });
            }
        }
    }
}
