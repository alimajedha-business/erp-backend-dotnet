using System.Text.Json.Serialization;

namespace NGErp.HCM.Service.DTOs;

public class OrganizationalStructureDto
{
    public Guid Id { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }
}

public class OrganizationalStructureTreeDto
{
    public Guid Id { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }
    public List<OrganizationalStructureTreeItemDto> Items { get; set; } = [];
}

public class CreateOrganizationStructureDto
{
    public required DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }
    public List<CreateOrganizationalStructureItemDto> Items { get; set; } = [];
}
