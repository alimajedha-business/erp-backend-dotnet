using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public class OrganizationalStructureDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; } = null!;
    public NodeType NodeType { get; set; }
    public string? Description { get; set; };
    public List<OrganizationalStructureDto> Children { get; set; } = new();
}
