using NGErp.General.Domain.Entities;

namespace NGErp.General.Service.DTOs;

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

public class UpdateMilitaryServiceStatusDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required MilitaryStatusType Type { get; set; }
}
