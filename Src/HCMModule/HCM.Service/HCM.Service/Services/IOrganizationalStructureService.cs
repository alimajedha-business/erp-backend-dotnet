using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

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

    Task<ListResponseModel<OrganizationalStructureDto>> GetAll(
        Guid companyId,
        OrganizationalStructureParameters parameters,
        CancellationToken ct
        );
}