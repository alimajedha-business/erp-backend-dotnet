using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IEmployeeWorkExperienceService
{
    Task<EmployeeWorkExperienceDto> CreateAsync(
        Guid employeeId,
        CreateEmployeeWorkExperienceDto createDto,
        CancellationToken ct
    );

    Task<EmployeeWorkExperienceDto> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeWorkExperienceDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeWorkExperienceParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeWorkExperienceDto> PatchAsync(
        Guid employeeId,
        Guid id,
        JsonPatchDocument<PatchEmployeeWorkExperienceDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid employeeId,
        Guid id,
        CancellationToken ct
    );
}
