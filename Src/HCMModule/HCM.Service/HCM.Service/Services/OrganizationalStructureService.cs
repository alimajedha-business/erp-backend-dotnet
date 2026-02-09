

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Service.Services;

public class OrganizationalStructureService(
    IOrganizationalStructureRepository organizationalStructureServiceRepository,

    ) : IOrganizationalStructureService
{
    private readonly IOrganizationalStructureRepository _organizationalStructureRepository = organizationalStructureServiceRepository;

    public async Task<List<OrganizationalStructureDto>> GetTreeAsync(Guid companyId, DateOnly date)
    {
        var nodes = _organizationalStructureRepository.Find(
            companyId, 
            x => x.ValidFrom <= date && x.ValidTo == null || x.ValidTo >= date);

     
    }
}


