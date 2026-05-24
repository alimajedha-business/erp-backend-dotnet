using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record EducationLevelDto(
    Guid Id,
    EducationLevelType Type,
    string Title
);

public record EducationLevelListDto(
    Guid Id,
    EducationLevelType Type,
    string Title
);

public class CreateEducationLevelDto
{
    public EducationLevelType Type { get; set; }
    public string Title { get; set; } = null!;
}

public class PatchEducationLevelDto
{
    public EducationLevelType? Type { get; set; }
    public string? Title { get; set; }
}
