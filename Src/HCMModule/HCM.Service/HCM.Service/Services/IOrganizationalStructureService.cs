using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationalStructureService
{
    Task<OrganizationalStructureTreeDto> GetTreeAtDateAsync(
        Guid companyId,
        DateOnly date,
        CancellationToken ct
        );

    Task<OrganizationalStructureTreeDto> GetCurrentTreeAsync(
        Guid companyId
        );

    // Task<Guid> SaveStructureVersionAsync(SaveOrganizationalStructureDto dto);
}