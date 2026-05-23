using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IEmployeeDependantService
{
    Task<EmployeeDependantDto> CreateAsync(
        CreateEmployeeDependantDto createDto,
        CancellationToken ct
    );

    Task<EmployeeDependantDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmployeeDependantDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeDependantParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EmployeeDependantDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEmployeeDependantDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
