using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class OrganizationalStructureService(
    IOrganizationalStructureRepository organizationalStructureServiceRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer
    ) : IOrganizationalStructureService
{
    private readonly IOrganizationalStructureRepository _organizationalStructureRepository = organizationalStructureServiceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;

    public Task<List<OrganizationalStructureDto>> GetCurrentTreeAsync(Guid companyId)
    {
        //var nodes = await _organizationalStructureRepository.Find(
        //    companyId, x =>  DateTime.Compare(x.EffectiveFrom,DateTime.Now.))
        //    .ToListAsync();
        //return _mapper.Map<List<OrganizationalStructureDto>>(nodes);
        throw new NotImplementedException();
    }

    public Task<List<OrganizationalStructureDto>> GetTreeAtDateAsync(Guid companyId, DateOnly date)
    {
        //var nodes = await _organizationalStructureRepository.Find(
        //    companyId,
        //    x => x.ValidFrom <= date && x.ValidTo == null || x.ValidTo >= date).ToListAsync();

        //return _mapper.Map<List<OrganizationalStructureDto>>(nodes);
        throw new NotImplementedException();
    }
}