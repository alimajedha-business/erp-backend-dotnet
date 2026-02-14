using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public class OrganizationalStructureTreeNodeDto
{
    public Guid ItemId { get; set; }
    public Guid NodeId { get; set; }
    public NodeType NodeType { get; set; }

    public string Title { get; set; } = default!; // DepartmentName or PositionTitle

    public Guid? ParentItemId { get; set; }

    public List<OrganizationalStructureTreeNodeDto> Children { get; set; }
        = new();
}

public class SaveOrganizationalStructureDto
{
    public DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }

    public List<StructureItemDto> Items { get; set; } = new();
}

public class StructureItemDto
{
    public Guid NodeId { get; set; }
    public Guid? ParentNodeId { get; set; }
}