using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record RelativeTypeDto(
    Guid Id,
    RelationshipType Type,
    string Title
);

public record RelativeTypeListDto(
    Guid Id,
    RelationshipType Type,
    string Title
);

public class CreateRelativeTypeDto
{
    public required RelationshipType Type { get; set; }
    public string Title { get; set; } = null!;
}

public class PatchRelativeTypeDto
{
    public RelationshipType? Type { get; set; }
    public string? Title { get; set; }
}
