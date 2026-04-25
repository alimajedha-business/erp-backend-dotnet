namespace NGErp.General.Service.DTOs;

public record MilitaryServiceStatusDto(
    Guid Id,
    string Name
);

public record MilitaryServiceStatusListDto(
    Guid Id,
    string Name
);

public class CreateMilitaryServiceStatusDto
{
    public required string Name { get; set; }
}

public class UpdateMilitaryServiceStatusDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
