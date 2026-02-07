using NGErp.General.Service.DTOs.PythonApi;
using Microsoft.Extensions.Logging;
using NGErp.Base.Service.Services;

namespace NGErp.General.Service.Services
{
    public interface IPythonIntegrationService
    {
        Task<List<CompanyDto>> GetCompaniesAsync(string? token = null);
        Task<CompanyDto?> GetCompanyByIdAsync(Guid id, string? token = null);
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto, string? token = null);
        Task<CompanyDto> UpdateCompanyAsync(Guid id, UpdateCompanyDto dto, string? token = null);
        Task<bool> DeleteCompanyAsync(Guid id, string? token = null);
    }

    public class PythonIntegrationService : IPythonIntegrationService
    {
        private readonly DjangoApiService _djangoApiService;
        private readonly ILogger<PythonIntegrationService> _logger;

        public PythonIntegrationService(
            DjangoApiService djangoApiService,
            ILogger<PythonIntegrationService> logger)
        {
            _djangoApiService = djangoApiService;
            _logger = logger;
        }

        public async Task<List<CompanyDto>> GetCompaniesAsync(string? token = null)
        {
            try
            {
                _logger.LogInformation("Fetching companies from Django API at /api/general/companies/");
                var result = await _djangoApiService.GetAsync<List<CompanyDto>>("/api/general/companies/", token);
                return result ?? new List<CompanyDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching companies from Django API");
                throw;
            }
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(Guid id, string? token = null)
        {
            try
            {
                _logger.LogInformation("Fetching company {CompanyId} from Django API at /api/general/companies/{CompanyId}/", id, id);
                return await _djangoApiService.GetAsync<CompanyDto>($"/api/general/companies/{id}/", token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching company {Id} from Django API", id);
                throw;
            }
        }

        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto, string? token = null)
        {
            try
            {
                _logger.LogInformation("Creating company in Django API: {Name}", dto.Name);
                var result = await _djangoApiService.PostAsync<CompanyDto>("/api/general/companies/", dto, token);
                return result ?? throw new Exception("Failed to create company - null response from Django");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company in Django API");
                throw;
            }
        }

        public async Task<CompanyDto> UpdateCompanyAsync(Guid id, UpdateCompanyDto dto, string? token = null)
        {
            try
            {
                _logger.LogInformation("Updating company {Id} in Django API", id);
                var result = await _djangoApiService.PatchAsync<CompanyDto>($"/api/general/companies/{id}/", dto, token);
                return result ?? throw new Exception($"Failed to update company {id} - null response from Django");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company {Id} in Django API", id);
                throw;
            }
        }

        public async Task<bool> DeleteCompanyAsync(Guid id, string? token = null)
        {
            try
            {
                _logger.LogInformation("Deleting company {Id} from Django API", id);
                return await _djangoApiService.DeleteAsync($"/api/general/companies/{id}/", token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company {Id} from Django API", id);
                throw;
            }
        }
    }
}
