using NGErp.General.Service.DTOs;

namespace NGErp.General.Service.Services;

public interface ICompanyService
{
    Task<CompanyDto> GetCompanyByIdAsync(Guid companyId, CancellationToken ct);
}
