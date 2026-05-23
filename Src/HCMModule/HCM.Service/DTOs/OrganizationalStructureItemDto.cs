using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public class OrganizationalStructureItemDto
{
}

public class OrganizationalStructureTreeItemDto
{
    public Guid Id { get; set; }
    public Guid? ParentItemId { get; set; }

    public OrganizationNodeTreeDto Node { get; set; } = null!;
    public List<OrganizationalStructureTreeItemDto> Children { get; set; } = [];
}

public class CreateOrganizationalStructureItemDto
{
    public Guid? ParentItemId { get; set; }
    public NodeType NodeType { get; init; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public List<CreateOrganizationalStructureItemDto> Children { get; set; } = [];
}
