using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationNodeService : IBaseServiceWithCompany<
    OrganizationNode,
    OrganizationNodeTreeDto,
    OrganizationNodeParameters,
    IOrganizationNodeRepository,
    HCMResource
    >
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

}