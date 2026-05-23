using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record MilitaryServiceStatusDto(
    Guid Id,
    string Title,
    MilitaryStatusType Type
);

public record MilitaryServiceStatusListDto(
    Guid Id,
    string Title,
    MilitaryStatusType Type
);

public class CreateMilitaryServiceStatusDto
{
    public required string Title { get; set; }
    public required MilitaryStatusType Type { get; set; }
}

public class PatchMilitaryServiceStatusDto
{
    public string? Title { get; set; }
    public MilitaryStatusType? Type { get; set; }
}
