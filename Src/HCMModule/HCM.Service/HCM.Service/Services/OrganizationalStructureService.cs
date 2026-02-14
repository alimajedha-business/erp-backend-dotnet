using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.HCM.Domain.Entities;
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

    public Task<Guid> SaveStructureVersionAsync(SaveOrganizationalStructureDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrganizationalStructureTreeNodeDto>> GetCurrentTreeAsync(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrganizationalStructureTreeNodeDto>> GetTreeAtDateAsync(Guid companyId, DateOnly date)
    {
        var items = await _organizationalStructureRepository.Find(companyId, x => x.EffectiveFrom == date)
         .Include(x => x.Items)
         .ThenInclude(n => n.)
         .Include(x => x.Node.Position)
         .Where(x => x.OrganizationalStructureId == structureId)
         .ToListAsync();

        var lookup = items.ToLookup(x => x.ParentItemId);

        OrganizationalStructureTreeNodeDto Build(Guid? parentId)
        {
            var nodes = lookup[parentId];

            return nodes.Select(x => new OrganizationalStructureTreeNodeDto
            {
                ItemId = x.Id,
                NodeId = x.NodeId,
                NodeType = x.Node.NodeType,
                Title = x.Node.NodeType == NodeType.Department
                    ? x.Node.Department!.Name
                    : x.Node.Position!.Title,
                ParentItemId = x.ParentItemId,
                Children = Build(x.Id).Children
            }).FirstOrDefault()!;
        }

        return Build(null);
    }
}