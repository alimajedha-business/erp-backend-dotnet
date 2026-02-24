using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class OrganizationalStructureService(
    IOrganizationalStructureRepository organizationalStructureServiceRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    ICompanyService companyService
    ) : IOrganizationalStructureService
{
    private readonly IOrganizationalStructureRepository _organizationalStructureRepository = organizationalStructureServiceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;
    private readonly ICompanyService _companyService = companyService;

    public async Task<OrganizationalStructureTreeDto> GetTreeAtDateAsync(Guid companyId, DateOnly date, CancellationToken ct)
    {
        var company = await _companyService.GetByIdAsync(companyId, ct);

        var structure = await _organizationalStructureRepository
            .Find(companyId, s => s.EffectiveFrom <= date)
            .AsNoTracking()
            .AsSingleQuery()
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Department)
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Position)
            .OrderByDescending(s => s.EffectiveFrom)
            .FirstOrDefaultAsync();
        var companyNode = new OrganizationalStructureTreeNodeDto
        {
            Id = Guid.Empty, // virtual
            ParentItemId = null,
            Node = new OrganizationNodeDto
            {
                Id = Guid.Empty,
                NodeType = NodeType.Department,
                Department = new DepartmentDto
                {
                    Id = Guid.Empty,
                    Name = company.Name,
                    Code = ""
                }
            },
            Children = []
        };

        if (structure == null)
        {
            return new OrganizationalStructureTreeDto
            {
                Id = Guid.Empty,
                EffectiveFrom = date,
                Items = [companyNode]
            };
        }

        var items = structure.Items!.Select(MapToTreeNodeDto).ToList();

        // Build tree hierarchy
        var rootItems = items.Where(x => x.ParentItemId == null).ToList();
        var childItems = items.Where(x => x.ParentItemId.HasValue).ToList();

        // Add children to parents
        foreach (var item in rootItems.Concat(childItems))
        {
            item.Children = childItems.Where(c => c.ParentItemId == item.Id).ToList();
        }

        companyNode.Children = rootItems;
        return new OrganizationalStructureTreeDto
        {
            Id = structure.Id,
            EffectiveFrom = structure.EffectiveFrom,
            Description = structure.Description,
            Items = [companyNode]
        };
    }

    public async Task<OrganizationalStructureTreeDto> SaveTreeAsync(
        Guid companyId,
        OrganizationalStructureTreeDto incomingTree,
        DateOnly effectiveFrom,
        string? description = null,
        CancellationToken ct = default)
    {
        var company = await _companyService.GetByIdAsync(companyId, ct);

        //if (effectiveFrom < DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)))
        //    throw new BusinessRuleException("Cannot modify or create structure versions in the past.");

        //// 1. Load current active structure at effectiveFrom (the one that would be seen just before this change)
        //var currentStructure = await GetActiveStructureAtDate(companyId, effectiveFrom, ct);

        //// 2. Load all potentially affected nodes (departments & positions) in one query
        //var allRelevantNodeIds = CollectAllNodeIdsFromTree(incomingTree);
        //var nodesDict = await _organizationNodeRepository
        //    .GetAllActiveNodesByIds(companyId, allRelevantNodeIds, effectiveFrom, ct)
        //    .ToDictionaryAsync(n => n.Id, ct);

        //// 3. Validate whole incoming tree
        //await ValidateIncomingTreeStructure(
        //    company,
        //    incomingTree,
        //    currentStructure,
        //    nodesDict,
        //    effectiveFrom,
        //    ct);

        // ────────────────────────────────────────────────────────────────
        // 4. Decide whether we need to create NEW version or can update future one
        // ────────────────────────────────────────────────────────────────
        OrganizationalStructure? structureToSave;

        var futureStructure = await _organizationalStructureRepository
            .Find(companyId, s => s.EffectiveFrom > effectiveFrom.AddDays(-1))
            .OrderBy(s => s.EffectiveFrom)
            .FirstOrDefaultAsync(ct);

        if (futureStructure != null && futureStructure.EffectiveFrom == effectiveFrom)
        {
            // We already have a draft/future version exactly on this date → update it
            structureToSave = futureStructure;
        }
        else
        {
            // Create brand new version
            structureToSave = new OrganizationalStructure
            {
                CompanyId = companyId,
                EffectiveFrom = effectiveFrom,
                Description = description,
                Items = new List<OrganizationalStructureItem>()
            };
        }

        // 5. Clear old items if we're overwriting
        //if (structureToSave.Items?.Any() == true)
        //{
        //    _context.RemoveRange(structureToSave.Items);
        //}

        //// 6. Build new flat list of OrganizationalStructureItem
        //var newItems = BuildFlatStructureItems(
        //    companyId,
        //    structureToSave,
        //    incomingTree.Items[0].Children,   // real roots
        //    nodesDict,
        //    parentItem: null);

        //structureToSave.Items = newItems;

        //if (structureToSave.Id == Guid.Empty)
        //{
        //    await _organizationalStructureRepository.AddAsync(structureToSave, ct);
        //}
        //// else → EF will see it as modified

        //await _context.SaveChangesAsync(ct);

        // 7. Return the saved tree (re-read or reconstruct)
        return await GetTreeAtDateAsync(companyId, effectiveFrom, ct);
    }

    private OrganizationalStructureTreeNodeDto MapToTreeNodeDto(OrganizationalStructureItem item)
    {
        return new OrganizationalStructureTreeNodeDto
        {
            Id = item.Id,
            //NodeId = item.NodeId,
            ParentItemId = item.ParentItemId,
            Node = new OrganizationNodeDto
            {
                Id = item.Node.Id,
                NodeType = item.Node.NodeType,
                //DepartmentId = item.Node.DepartmentId,
                //PositionId = item.Node.PositionId,
                Department = item.Node.Department != null ? new DepartmentDto
                {
                    Id = item.Node.Department.Id,
                    Name = item.Node.Department.Name,
                    Code = item.Node.Department.Code,
                } : null,
                Position = item.Node.Position != null ? new PositionDto
                {
                    Id = item.Node.Position.Id,
                    Name = item.Node.Position.Name,
                    Code = item.Node.Position.Code,
                } : null
            },
            Children = []
        };
    }
}