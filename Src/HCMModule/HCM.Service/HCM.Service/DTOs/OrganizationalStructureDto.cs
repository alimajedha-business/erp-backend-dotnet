using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public class OrganizationalStructureDto
{
    public Guid Id { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }
}

public class OrganizationalStructureTreeNodeDto
{
    public Guid Id { get; set; }

    // public Guid NodeId { get; set; }
    public Guid? ParentItemId { get; set; }

    public OrganizationNodeDto Node { get; set; } = null!;
    public List<OrganizationalStructureTreeNodeDto> Children { get; set; } = [];
}

public class OrganizationalStructureTreeDto
{
    public Guid Id { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }
    public List<OrganizationalStructureTreeNodeDto> Items { get; set; } = [];
}

public record OrganizationNodeDto
{
    public Guid Id { get; init; }
    public NodeType NodeType { get; init; }
    //public Guid? DepartmentId { get; set; }
    //public Guid? PositionId { get; set; }
    public DepartmentDto? Department { get; set; }
    public PositionDto? Position { get; set; }
}