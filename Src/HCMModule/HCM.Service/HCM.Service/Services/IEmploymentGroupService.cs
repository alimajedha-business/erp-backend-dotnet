using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public interface IEmploymentGroupService
{

    Task<EmploymentGroupDto> CreateAsync(
    Guid companyId,
    CreateEmploymentGroupDto createDto,
    CancellationToken ct
);

    Task<EmploymentGroupDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EmploymentGroupDto>> GetFilteredAsync(
        Guid companyId,
        EmploymentGroupParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    //Task<EmploymentGroupDto> PatchAsync(
    //    Guid companyId,
    //    Guid id,
    //    JsonPatchDocument<PatchPositionDto> patchDocument,
    //    CancellationToken ct
    //);

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        PositionChangeStatusDto changeStatusDto,
        CancellationToken ct
    );
}