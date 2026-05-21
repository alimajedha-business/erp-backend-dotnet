using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record EducationalStatusDto(
    Guid Id,
    EducationalStatusType Type,
    string Title
);

public record EducationalStatusListDto(
    Guid Id,
    int Type,
    string Title
);

public class CreateEducationalStatusDto
{
    public EducationalStatusType Type { get; set; }
    public string Title { get; set; } = null!;
}

public class PatchEducationalStatusDto
{
    public EducationalStatusType? Type { get; set; }
    public string? Title { get; set; }
}
