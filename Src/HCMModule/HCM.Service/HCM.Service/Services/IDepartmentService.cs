using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IDepartmentService
{
    Task<ListResponseModel<DepartmentDto>> GetAllDepartmentsAsync(
        Guid companyId,
        DepartmentParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
        );

    Task<DepartmentDto> GetDepartmentByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        );

    Task<DepartmentDto> CreateDepartmentAsync(
        Guid companyId,
        CreateDepartmentDto createDto,
        CancellationToken ct
        );

    Task<DepartmentDto> PatchDepartmentAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchDepartmentDto> patchDocument,
         CancellationToken ct
        );

    Task DeleteDepartmentAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        );

    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool NewStatus,
        CancellationToken ct
        );
}