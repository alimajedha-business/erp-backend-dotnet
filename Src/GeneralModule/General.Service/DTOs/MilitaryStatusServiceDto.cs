namespace NGErp.General.Service.DTOs;

public record MilitaryServiceStatusDto(
    long Id,
    string Name
);

public record MilitaryServiceStatusListDto(
    long Id,
    string Name
);

public class CreateMilitaryServiceStatusDto
{
    public required string Name { get; set; }
}

public class UpdateMilitaryServiceStatusDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
}
