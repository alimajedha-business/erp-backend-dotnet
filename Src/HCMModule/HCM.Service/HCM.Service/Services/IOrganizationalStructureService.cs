using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationalStructureService
{
    Task<OrganizationalStructureTreeDto> GetTreeAtDateAsync(
        Guid companyId,
        DateOnly date,
        CancellationToken ct
        );

    Task<OrganizationalStructureTreeDto> SaveTreeAsync(
        Guid companyId,
        OrganizationalStructureTreeDto incomingTree,
        DateOnly effectiveFrom,
        string? description = null,
        CancellationToken ct = default
        );

    Task<IEnumerable<OrganizationalStructureDto>> GetTreeAtDateAsync(
        Guid companyId,
        CancellationToken ct
        );
}