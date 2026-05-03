using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEmployeeRelativeService
{
    Task<EmployeeRelativeDto> CreateAsync(
        Guid companyId,
        CreateEmployeeRelativeDto createDto,
        CancellationToken ct
    );

    Task<EmployeeRelativeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeRelativeDto>> GetFilteredAsync(
        Guid companyId,
        EmployeeRelativeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeRelativeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchEmployeeRelativeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
