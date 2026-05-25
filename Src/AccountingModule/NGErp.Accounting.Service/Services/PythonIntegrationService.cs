using Microsoft.Extensions.Logging;

using NGErp.Accounting.Service.DTOs;
using NGErp.Base.Service.Services;

namespace NGErp.Accounting.Service.Services;

public interface IPythonIntegrationService
{
    Task<SlaveAccountCompanyDto?> GetSlaveAccountCompanyByIdAsync(
        Guid id,
        string? token = null
    );
}

public class PythonIntegrationService(
    DjangoApiService djangoApiService,
    ILogger<PythonIntegrationService> logger
) : IPythonIntegrationService
{
    private readonly DjangoApiService _djangoApiService = djangoApiService;
    private readonly ILogger<PythonIntegrationService> _logger = logger;

    public async Task<SlaveAccountCompanyDto?> GetSlaveAccountCompanyByIdAsync(
        Guid id,
        string? token = null
    )
    {
        // TODO: fix api route when the api is being implemented in django
        try
        {
            _logger.LogInformation("Fetching company {CompanyId} from Django API at /api/general/companies/{CompanyId}/", id, id);
            return await _djangoApiService.GetAsync<SlaveAccountCompanyDto>($"/api/general/companies/{id}/", token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching company {Id} from Django API", id);
            throw;
        }
    }
}
