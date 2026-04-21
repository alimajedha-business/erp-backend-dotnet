using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationNodeService 

{
    Task<OrganizationNodeTreeDto> GetOrCreateAsync(
    Guid companyId,
    CreateOrganizationNodeDto item,
    CancellationToken ct = default
    );
    
    Task<OrganizationNodeTreeDto?> GetByDepartmentIdAsync(
    Guid companyId,
    Guid departmentId,
    CancellationToken ct = default
    );

//    Task<PositionDto> CreateAsync(
//    Guid companyId,
//    CreatePositionDto createDto,
//    CancellationToken ct
//);

//    Task<PositionDto> GetByIdAsync(
//        Guid companyId,
//        Guid id,
//        bool trackChanges = false,
//        CancellationToken ct = default
//    );

    //Task<ListResponseModel<PositionDto>> GetFilteredAsync(
    //    Guid companyId,
    //    PositionParameters parameters,
    //    FilterNodeDto? filterNodeDto = null,
    //    CancellationToken ct = default
    //);

    //Task<PositionDto> PatchAsync(
    //    Guid companyId,
    //    Guid id,
    //    JsonPatchDocument<PatchPositionDto> patchDocument,
    //    CancellationToken ct
    //);

    //Task DeleteAsync(
    //    Guid companyId,
    //    Guid id,
    //    CancellationToken ct
    //);

    //Task ChangeStatusAsync(
    //    Guid companyId,
    //    Guid id,
    //    PositionChangeStatusDto changeStatusDto,
    //    CancellationToken ct
    //);

}