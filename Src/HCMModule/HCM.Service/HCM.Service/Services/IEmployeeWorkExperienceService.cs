using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEmployeeWorkExperienceService
{
    Task<EmployeeWorkExperienceDto> CreateAsync(
        CreateEmployeeWorkExperienceDto createDto,
        CancellationToken ct
    );

    Task<EmployeeWorkExperienceDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeWorkExperienceDto>> GetFilteredAsync(
        EmployeeWorkExperienceParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeWorkExperienceDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEmployeeWorkExperienceDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
