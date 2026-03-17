using NGErp.General.Service.DTOs;

namespace NGErp.General.Service.Services;

public interface ICompanyUnitService
{
    Task<CompanyUnitDto> GetByIdAsync(
        Guid companyId,
        Guid companyUnitId,
        CancellationToken ct
    );
}
