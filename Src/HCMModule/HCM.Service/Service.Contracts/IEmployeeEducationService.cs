using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IEmployeeEducationService
{
    Task<EmployeeEducationDto> CreateAsync(
        Guid employeeId,
        CreateEmployeeEducationDto createDto,
        CancellationToken ct
    );

    Task<EmployeeEducationDto> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeEducationDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeEducationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeEducationDto> PatchAsync(
        Guid employeeId,
        Guid id,
        JsonPatchDocument<PatchEmployeeEducationDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid employeeId,
        Guid id,
        CancellationToken ct
    );
}
