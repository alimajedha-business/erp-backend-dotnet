using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Specifications;

namespace NGErp.HCM.Service.Services;

public class OrganizationalStructureService(
    IOrganizationalStructureRepository organizationalStructureServiceRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    ICompanyService companyService,
    IDepartmentService departmentService,
    IPositionService positiontService,
    IOrganizationNodeService organizationNodeService
    ) : IOrganizationalStructureService
{
    private readonly IOrganizationalStructureRepository _organizationalStructureRepository = organizationalStructureServiceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;
    private readonly ICompanyService _companyService = companyService;
    private readonly IDepartmentService _departmentService= departmentService;
    private readonly IPositionService _positionService= positiontService;
    private readonly IOrganizationNodeService _organizationNodeService = organizationNodeService;

//correct
    public async Task<ListResponseModel<OrganizationalStructureDto>> GetAll(
        Guid companyId,
        OrganizationalStructureParameters parameters,
        CancellationToken ct
        )
    {
        await _companyService.GetByIdAsync(companyId, ct);
        var query = _organizationalStructureRepository.FilterByQ(companyId, parameters);
        var res = await _organizationalStructureRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<OrganizationalStructureDto>(
           results: _mapper.Map<IReadOnlyList<OrganizationalStructureDto>>(res.items),
           totalCount: res.count,
           parameters
       );
    }
    //correct
    public async Task<OrganizationalStructureTreeDto> GetTreeAtDateAsync(
        Guid companyId,
        DateOnly date,
        CancellationToken ct)
    {
        var company = await _companyService.GetByIdAsync(companyId, ct);

        var structure = await _organizationalStructureRepository
            .Find(companyId, s => s.EffectiveFrom <= date)
            .AsNoTracking()
            .AsSingleQuery()
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node!)
                    .ThenInclude(n => n.Department)
            .Include(s => s.Items!)
                .ThenInclude(i => i.Node!)
                    .ThenInclude(n => n.Position)
            .OrderByDescending(s => s.EffectiveFrom)
            .FirstOrDefaultAsync(ct);

        var companyNode = new OrganizationalStructureTreeItemDto
        {
            Id = Guid.Empty,
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

        var items = structure.Items?
            .Where(x => x.Node != null)
            .Select(MapToTreeNodeDto)
            .ToList() ?? [];

        var rootItems = items.Where(x => x.ParentItemId == null).ToList();
        var childItems = items.Where(x => x.ParentItemId.HasValue).ToList();

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
    public async Task CreateOrganizationalStructureItemsAsync(
        OrganizationalStructure organizationalStructure,
        List<CreateOrganizationalStructureItemDto> items,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(organizationalStructure);
        ArgumentNullException.ThrowIfNull(items);

        var usedDepartmentIds = new HashSet<Guid>();

        await ProcessStructureItemsAsync(
            organizationalStructure,
            items,
            parentItemId: null,
            parentNodeType: null,
            usedDepartmentIds,
            ct);
    }

    private async Task ProcessStructureItemsAsync(
        OrganizationalStructure organizationalStructure,
        List<CreateOrganizationalStructureItemDto> items,
        Guid? parentItemId,
        NodeType? parentNodeType,
        HashSet<Guid> usedDepartmentIds,
        CancellationToken ct)
    {
        if (items == null || items.Count == 0)
            return;

        var siblingPositionIds = new HashSet<Guid>();
        var positionChildrenCount = 0;

        foreach (var item in items)
        {
            ValidateNodeIdentity(item);

            switch (item.NodeType)
            {
                case NodeType.Department:
                    ValidateDepartmentNode(
                        item,
                        parentNodeType,
                        usedDepartmentIds);
                    break;

                case NodeType.Position:
                    ValidatePositionNode(
                        item,
                        parentNodeType,
                        siblingPositionIds,
                        ref positionChildrenCount);
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported node type '{item.NodeType}'.");
            }

            await ValidateNodeStatusAsync(
                organizationalStructure.CompanyId,
                organizationalStructure.EffectiveFrom,
                item,
                ct);

            var node = await _organizationNodeService.GetOrCreateAsync(
                organizationalStructure.CompanyId,
                new CreateOrganizationNodeDto
                {
                    NodeType = item.NodeType,
                    DepartmentId = item.DepartmentId,
                    PositionId = item.PositionId
                },
                ct);

            var structureItem = new OrganizationalStructureItem
            {
                Id = Guid.NewGuid(),
                CompanyId = organizationalStructure.CompanyId,
                OrganizationalStructureId = organizationalStructure.Id,
                OrganizationalStructure = organizationalStructure,
                NodeId = node.Id,
                ParentItemId = parentItemId
            };

            organizationalStructure.Items.Add(structureItem);

            if (item.Children is { Count: > 0 })
            {
                await ProcessStructureItemsAsync(
                    organizationalStructure,
                    item.Children,
                    structureItem.Id,
                    item.NodeType,
                    usedDepartmentIds,
                    ct);
            }
        }
    }

    private static void ValidateNodeIdentity(CreateOrganizationalStructureItemDto item)
    {
        var hasDepartment = item.DepartmentId.HasValue;
        var hasPosition = item.PositionId.HasValue;

        if (hasDepartment == hasPosition)
        {
            throw new InvalidOperationException(
                "Each structure item must have exactly one of DepartmentId or PositionId.");
        }
    }

    private static void ValidateDepartmentNode(
        CreateOrganizationalStructureItemDto item,
        NodeType? parentNodeType,
        HashSet<Guid> usedDepartmentIds)
    {
        if (!item.DepartmentId.HasValue)
        {
            throw new InvalidOperationException(
                "A department node must have DepartmentId.");
        }

        if (parentNodeType == NodeType.Position)
        {
            throw new InvalidOperationException(
                "A department node cannot have a parent of type position.");
        }

        if (!usedDepartmentIds.Add(item.DepartmentId.Value))
        {
            throw new InvalidOperationException(
                $"DepartmentId '{item.DepartmentId}' is duplicated in the tree.");
        }
    }

    private static void ValidatePositionNode(
        CreateOrganizationalStructureItemDto item,
        NodeType? parentNodeType,
        HashSet<Guid> siblingPositionIds,
        ref int positionChildrenCount)
    {
        if (!item.PositionId.HasValue)
        {
            throw new InvalidOperationException(
                "A position node must have PositionId.");
        }

        if (!siblingPositionIds.Add(item.PositionId.Value))
        {
            throw new InvalidOperationException(
                $"PositionId '{item.PositionId}' is duplicated among sibling nodes.");
        }

        if (parentNodeType == NodeType.Department)
        {
            positionChildrenCount++;

            if (positionChildrenCount > 1)
            {
                throw new InvalidOperationException(
                    "A department node can have at most one position child.");
            }
        }
    }

    private async Task ValidateNodeStatusAsync(
        Guid companyId,
        DateOnly effectiveFrom,
        CreateOrganizationalStructureItemDto item,
        CancellationToken ct)
    {
        if (item.DepartmentId.HasValue)
        {
            var department = await _departmentService.GetByIdAsync(
                companyId,
                item.DepartmentId.Value,
                ct);

            ValidateEffectiveDateAgainstStatus(
                entityType: "Department",
                entityName: department.Name,
                status: department.Status,
                statusChangeDate: department.StatusChangeDate,
                effectiveFrom: effectiveFrom);
        }

        if (item.PositionId.HasValue)
        {
            var position = await _positionService.GetByIdAsync(
                companyId,
                item.PositionId.Value,
                ct);

            ValidateEffectiveDateAgainstStatus(
                entityType: "Position",
                entityName: position.Name,
                status: position.Status,
                statusChangeDate: position.StatusChangeDate,
                effectiveFrom: effectiveFrom);
        }
    }

    private static void ValidateEffectiveDateAgainstStatus(
        string entityType,
        string? entityName,
        bool status,
        DateTime? statusChangeDate,
        DateOnly effectiveFrom)
    {
        if (status)
            return;

        if (!statusChangeDate.HasValue)
        {
            throw new InvalidOperationException(
                $"{entityType} '{entityName}' is inactive and has no StatusChangeDate.");
        }

        var deactivationDate = DateOnly.FromDateTime(statusChangeDate.Value);

        if (effectiveFrom > deactivationDate)
        {
            throw new InvalidOperationException(
                $"{entityType} '{entityName}' is inactive from {deactivationDate:yyyy-MM-dd} and cannot be used for structure effective date {effectiveFrom:yyyy-MM-dd}.");
        }
    }
    public async Task<OrganizationalStructureTreeDto> CreateAsync(
        Guid companyId,
        CreateOrganizationStructureDto incomingTree,
        CancellationToken ct = default)
    {
        DateOnly effectiveFrom = incomingTree.EffectiveFrom;
        List<CreateOrganizationalStructureItemDto> items = incomingTree.Items ?? [];

        var company = await _companyService.GetByIdAsync(companyId, ct);

        // 2️ Load all nodes (for validation)
        var allNodes = await _organizationalStructureRepository
            .GetAllAsync(companyId, includeQuery, ct);

        // 3️ Validate Tree
        //ValidateTree(incomingTree, allNodes.items, effectiveFrom);

        // 4️⃣ Create new structure (history preservation)
        var structure = new OrganizationalStructure
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            EffectiveFrom = effectiveFrom,
            Description = incomingTree.Description
        };

        await CreateOrganizationalStructureItemsAsync(structure, items, ct);

        await _organizationalStructureRepository.AddAsync(structure, ct);
        await _organizationalStructureRepository.SaveChangesAsync(ct);

        return await GetTreeAtDateAsync(companyId, effectiveFrom, ct);
    }

    private OrganizationalStructureTreeItemDto MapToTreeNodeDto(OrganizationalStructureItem item)
    {
        var node = item.Node ?? throw new InvalidOperationException(
            $"OrganizationalStructureItem '{item.Id}' has no Node.");

        return new OrganizationalStructureTreeItemDto
        {
            Id = item.Id,
            ParentItemId = item.ParentItemId,
            Node = new OrganizationNodeTreeDto
            {
                Id = node.Id,
                NodeType = node.NodeType,
                Department = node.Department != null
                    ? new DepartmentDto
                    {
                        Id = node.Department.Id,
                        Name = node.Department.Name,
                        Code = node.Department.Code,
                    }
                    : null,
                Position = node.Position != null
                    ? new PositionDto
                    {
                        Id = node.Position.Id,
                        Name = node.Position.Name,
                        Code = node.Position.Code,
                    }
                    : null
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

//    private static IQueryable<OrganizationalStructure> includeQuery(
//    IQueryable<OrganizationalStructure> q
//) => q.Include(s => s.Items!)
//                .ThenInclude(i => i.Node)
//                    .ThenInclude(n => n.Department)
//            .Include(s => s.Items!)
//                .ThenInclude(i => i.Node)
//                    .ThenInclude(n => n.Position);
}