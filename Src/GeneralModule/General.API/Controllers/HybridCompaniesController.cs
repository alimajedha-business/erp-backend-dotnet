using Microsoft.AspNetCore.Mvc;
using NGErp.General.Infrastructure.Services;
using NGErp.General.Service.DTOs.PythonApi;
using ERP.API.Services;
using Microsoft.Extensions.Logging;

namespace NGErp.General.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HybridCompaniesController : ControllerBase
    {
        private readonly IPythonIntegrationService _pythonIntegrationService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<HybridCompaniesController> _logger;

        public HybridCompaniesController(
            IPythonIntegrationService pythonIntegrationService,
            ICurrentUserService currentUserService,
            ILogger<HybridCompaniesController> logger)
        {
            _pythonIntegrationService = pythonIntegrationService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        /// <summary>
        /// Get all companies from Django/Python API
        /// This is a simplified version - you can extend it to also fetch from .NET SQL database
        /// </summary>
        [HttpGet("all-sources")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                _logger.LogInformation("Fetching companies from Django API");

                var djangoCompanies = await _pythonIntegrationService.GetCompaniesAsync(
                    _currentUserService.Token
                );

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        django = new
                        {
                            source = "Django/Python API",
                            count = djangoCompanies.Count,
                            companies = djangoCompanies
                        }
                    },
                    totalCount = djangoCompanies.Count,
                    message = "Add .NET SQL database calls by injecting IGeneralServiceManager"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching companies from Django API");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Failed to fetch companies",
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Example endpoint showing how to work with Python API data
        /// </summary>
        [HttpGet("compare/{id}")]
        public async Task<IActionResult> CompareCompany(int id)
        {
            try
            {
                var djangoCompany = await _pythonIntegrationService.GetCompanyByIdAsync(
                    id,
                    _currentUserService.Token
                );

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        existsInDjango = djangoCompany != null,
                        djangoCompany,
                        message = "To compare with .NET database, inject IGeneralServiceManager"
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching company {Id}", id);
                return StatusCode(500, new
                {
                    success = false,
                    error = $"Failed to fetch company {id}",
                    message = ex.Message
                });
            }
        }
    }
}
