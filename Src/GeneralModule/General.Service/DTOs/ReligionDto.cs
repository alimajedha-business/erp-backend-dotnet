namespace NGErp.General.Service.DTOs;

public record ReligionDto(
    Guid Id,
    string Name
);

public record ReligionListDto(
    Guid Id,
    string Name
);

public class CreateReligionDto
{
    public required string Name { get; set; }
}

public class PatchReligionDto
{
    public string? Name { get; set; }
}
