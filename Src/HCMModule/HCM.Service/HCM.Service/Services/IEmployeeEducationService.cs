using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEmployeeEducationService
{
    Task<EmployeeEducationDto> CreateAsync(
        CreateEmployeeEducationDto createDto,
        CancellationToken ct
    );

    Task<EmployeeEducationDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeEducationDto>> GetFilteredAsync(
        EmployeeEducationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeEducationDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEmployeeEducationDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
