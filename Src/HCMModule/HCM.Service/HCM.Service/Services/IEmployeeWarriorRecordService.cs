using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEmployeeWarriorRecordService
{
    Task<EmployeeWarriorRecordDto> CreateAsync(
        Guid employeeId,
        CreateEmployeeWarriorRecordDto createDto,
        CancellationToken ct
    );

    Task<EmployeeWarriorRecordDto> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeWarriorRecordDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeWarriorRecordParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeWarriorRecordDto> PatchAsync(
        Guid employeeId,
        Guid id,
        JsonPatchDocument<PatchEmployeeWarriorRecordDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid employeeId,
        Guid id,
        CancellationToken ct
    );
}
