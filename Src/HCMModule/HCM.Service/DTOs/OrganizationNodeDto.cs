using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record CreateOrganizationNodeDto
{
    public Guid Id { get; init; }
    public NodeType NodeType { get; init; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
}

public record OrganizationNodeTreeDto
{
    public Guid Id { get; init; }
    public NodeType NodeType { get; init; }

    public DepartmentDto? Department { get; set; }
    public PositionDto? Position { get; set; }
}