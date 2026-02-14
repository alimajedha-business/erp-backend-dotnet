using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationalStructureService
{
    Task<List<OrganizationalStructureTreeNodeDto>> GetTreeAtDateAsync(
        Guid companyId,
        DateOnly date
        );

    Task<List<OrganizationalStructureTreeNodeDto>> GetCurrentTreeAsync(
        Guid companyId
        );

    Task<Guid> SaveStructureVersionAsync(SaveOrganizationalStructureDto dto);
}