namespace NGErp.HCM.Service.DTOs;

public record EducationFieldDto(
    Guid Id,
    string Title
);

public record EducationFieldListDto(
    Guid Id,
    string Title
);

public class CreateEducationFieldDto
{
    public string Title { get; set; } = null!;
}

public class PatchEducationFieldDto
{
    public string? Title { get; set; }
}
