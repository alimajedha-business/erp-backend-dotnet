using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
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

    public async Task<ListResponseModel<OrganizationalStructureDto>> GetAll(
        Guid companyId,
        OrganizationalStructureParameters parameters,
        CancellationToken ct
        )
    {
        await _companyService.GetByIdAsync(companyId, ct);
        var listQueryResult = await _organizationalStructureRepository.GetAllAsync(companyId, parameters, ct);

        return new ListResponseModel<OrganizationalStructureDto>(
           items: _mapper.Map<IReadOnlyList<OrganizationalStructureDto>>(listQueryResult.items),
           totalCount: listQueryResult.count,
           parameters
       );
    }

    public async Task<OrganizationalStructureTreeDto> GetTreeAtDateAsync(
        Guid companyId,
        DateOnly date,
        CancellationToken ct
        )
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
        var companyNode = new OrganizationalStructureTreeItemDto
        {
            Id = Guid.Empty, // virtual
            ParentItemId = null,
            Node = new OrganizationNodeTreeDto
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
        CreateOrganizationStructureDto incomingTree,
        DateOnly effectiveFrom,
        string? description = null,
        CancellationToken ct = default)
    {
        // 1️ Validate company exists
        var company = await _companyService.GetByIdAsync(companyId, ct);

        // 2️ Load all nodes (for validation)
        var allNodes = await _organizationalStructureRepository
            .GetAllAsync(companyId, includeQuery, ct);

        // 3️ Validate Tree
        // ValidateTree(newTree, allNodes.items, effectiveFrom);

        // 4️⃣ Create new structure (history preservation)
        var structure = new OrganizationalStructure
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            EffectiveFrom = effectiveFrom,
            Description = incomingTree.Description,
            Items = new List<OrganizationalStructureItem>()
        };

        // 5️⃣ Flatten tree and build items
        foreach (var root in incomingTree.Items)
        {
            foreach (var child in root.Children)
            {
                //  BuildItemsRecursive(child, null, structure);
            }
        }

        await _organizationalStructureRepository.AddAsync(structure, ct);
        //await _unitOfWork.SaveChangesAsync(ct);

        return await GetTreeAtDateAsync(companyId, effectiveFrom, ct);
    }

    private OrganizationalStructureTreeItemDto MapToTreeNodeDto(OrganizationalStructureItem item)
    {
        return new OrganizationalStructureTreeItemDto
        {
            Id = item.Id,
            //NodeId = item.NodeId,
            ParentItemId = item.ParentItemId,
            Node = new OrganizationNodeTreeDto
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

    private void BuildItemsRecursive(
    OrganizationalStructureTreeItemDto dto,
    OrganizationalStructureItem? parent,
    OrganizationalStructure structure)
    {
        //var item = new OrganizationalStructureItem
        //{
        //    Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
        //    CompanyId = structure.CompanyId,
        //    OrganizationalStructureId = structure.Id,
        //    NodeId = dto.Node.Id,
        //    ParentItemId = parent?.Id,
        //    Children = new List<OrganizationalStructureItem>()
        //};

        //structure.Items!.Add(item);

        //foreach (var child in dto.Children)
        //{
        //    BuildItemsRecursive(child, item, structure);
        //}
        throw new NotImplementedException();
    }

    private void ValidateTree(
    OrganizationalStructureTreeDto tree,
    List<OrganizationNode> allNodes,
    DateOnly effectiveFrom)
    {
        var flatList = Flatten(tree);

        ValidateParentChildRules(flatList, allNodes);
        ValidateNoDuplicateUnits(flatList, allNodes);
        ValidateNoDuplicatePositionsUnderParent(flatList);
        ValidateNoCircularReference(tree);
    }

    private List<OrganizationalStructureTreeItemDto> Flatten(
    OrganizationalStructureTreeDto tree)
    {
        var result = new List<OrganizationalStructureTreeItemDto>();

        void Traverse(OrganizationalStructureTreeItemDto node)
        {
            result.Add(node);
            foreach (var child in node.Children)
                Traverse(child);
        }

        foreach (var root in tree.Items)
            Traverse(root);

        return result;
    }

    private void ValidateParentChildRules(
    List<OrganizationalStructureTreeItemDto> nodes,
    List<OrganizationNode> allNodes)
    {
        var lookup = nodes.ToDictionary(x => x.Id);

        foreach (var node in nodes.Where(x => x.ParentItemId != null))
        {
            var parent = lookup[node.ParentItemId!.Value];

            var parentType = parent.Node.NodeType;
            var childType = node.Node.NodeType;

            if (parentType == NodeType.Position && childType != NodeType.Position)
                throw new Exception("Position node can only have Position children.");

            if (parentType == NodeType.Department)
            {
                if (childType == NodeType.Position)
                {
                    var positionChildrenCount = nodes
                        .Count(x => x.ParentItemId == parent.Id &&
                                    x.Node.NodeType == NodeType.Position);

                    if (positionChildrenCount > 1)
                        throw new Exception("Department can only have one Position.");
                }
            }
        }
    }

    private void ValidateNoDuplicateUnits(
    List<OrganizationalStructureTreeItemDto> nodes,
    List<OrganizationNode> allNodes)
    {
        var departmentIds = nodes
            .Where(x => x.Node.NodeType == NodeType.Department)
            .Select(x => x.Node.Id);

        if (departmentIds.Count() != departmentIds.Distinct().Count())
            throw new Exception("Duplicate department in tree is not allowed.");
    }

    private void ValidateNoDuplicatePositionsUnderParent(
    List<OrganizationalStructureTreeItemDto> nodes)
    {
        var duplicates = nodes
            .Where(x => x.Node.NodeType == NodeType.Position)
            .GroupBy(x => new { x.ParentItemId, x.Node.Id })
            .Where(g => g.Count() > 1);

        if (duplicates.Any())
            throw new Exception("Duplicate position under same parent.");
    }

    private void ValidateNoCircularReference(
    OrganizationalStructureTreeDto tree)
    {
        var visited = new HashSet<Guid>();

        void DFS(OrganizationalStructureTreeItemDto node, HashSet<Guid> path)
        {
            if (path.Contains(node.Id))
                throw new Exception("Circular reference detected.");

            path.Add(node.Id);

            foreach (var child in node.Children)
                DFS(child, new HashSet<Guid>(path));
        }

        foreach (var root in tree.Items)
            DFS(root, new HashSet<Guid>());
    }

    private static IQueryable<OrganizationalStructure> includeQuery(
    IQueryable<OrganizationalStructure> q
) => q.Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Department)
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node)
                    .ThenInclude(n => n.Position);
}