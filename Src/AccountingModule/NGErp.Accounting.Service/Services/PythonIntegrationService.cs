using Microsoft.Extensions.Logging;

using NGErp.Accounting.Service.DTOs;
using NGErp.Base.Service.Services;

namespace NGErp.Accounting.Service.Services;

public interface IPythonIntegrationService
{
    Task<SlaveAccountCompanyDto?> GetSlaveAccountCompanyByIdAsync(
        Guid companyId,
        Guid ledgerId,
        Guid? slaveAccountCompanyId
    );
}

public class PythonIntegrationService(
    DjangoApiService djangoApiService,
    ICurrentUserService currentUserService,
    ILogger<PythonIntegrationService> logger
) : IPythonIntegrationService
{
    private readonly DjangoApiService _djangoApiService = djangoApiService;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ILogger<PythonIntegrationService> _logger = logger;

    public async Task<SlaveAccountCompanyDto?> GetSlaveAccountCompanyByIdAsync(
        Guid companyId,
        Guid ledgerId,
        Guid? slaveAccountCompanyId
    )
    {
        if (slaveAccountCompanyId is null)
            throw new Exception();

        try
        {
            _logger.LogInformation("Fetching slave account company from Django API");
            return await _djangoApiService.GetAsync<SlaveAccountCompanyDto>(
                $"/api/accounting/{companyId}/accounts/ledgers/{ledgerId}/company-slaves/{slaveAccountCompanyId}/",
                _currentUserService.Token
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching slave account company"); 
            throw;
        }
    }
}
