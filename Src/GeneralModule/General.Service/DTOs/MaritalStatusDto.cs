using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record MaritalStatusDto(
    Guid Id,
    MaritalStatusType Type,
    string Title
);

public record MaritalStatusListDto(
    Guid Id,
    MaritalStatusType Type,
    string Title
);

public class CreateMaritalStatusDto
{
    public MaritalStatusType Type { get; set; }
    public string Title { get; set; } = null!;
}

public class PatchMaritalStatusDto
{
    public MaritalStatusType? Type { get; set; }
    public string? Title { get; set; }
}

