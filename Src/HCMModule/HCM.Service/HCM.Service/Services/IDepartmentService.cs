using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IDepartmentService
{
    Task<DepartmentDto> CreateAsync(
        Guid companyId,
        CreateDepartmentDto createDto,
        CancellationToken ct
    );

    Task<DepartmentDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<DepartmentDto>> GetAllAsync(
        Guid companyId,
        DepartmentParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<DepartmentDto>> GetAllAsync(
        Guid companyId,
        DepartmentParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<DepartmentDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchDepartmentDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        DepartmentChangeStatusDto changeStatusDto,
        CancellationToken ct
    );
}