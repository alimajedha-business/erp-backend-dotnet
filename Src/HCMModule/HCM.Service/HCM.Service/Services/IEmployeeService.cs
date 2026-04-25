using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEmployeeService
{
    Task<EmployeeDto> CreateAsync(
    Guid companyId,
    CreateEmployeeDto createDto,
    CancellationToken ct
);

    Task<EmployeeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeDto>> GetFilteredAsync(
        Guid companyId,
        EmployeeParamaters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchEmployeeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}


