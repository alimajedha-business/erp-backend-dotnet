using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeWorkExperienceDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeBaseDto? Employee { get; set; }
    [MaxLength(200)]
    [MinLength(2)]
    public string CompanyName { get; set; } = default!;
    [MaxLength(150)]
    [MinLength(2)]
    public string Position { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class CreateEmployeeWorkExperienceDto
{
    [Required]
    public Guid? EmployeeId { get; set; }
    [Required]
    [MaxLength(200)]
    [MinLength(2)]
    public required string CompanyName { get; set; }
    [MaxLength(150)]
    [MinLength(2)]
    public required string Position { get; set; }
    [Required]
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class PatchEmployeeWorkExperienceDto
{
    public Guid? EmployeeId { get; set; }
    [MaxLength(200)]
    [MinLength(2)]
    public string? CompanyName { get; set; }
    [MaxLength(150)]
    [MinLength(2)]
    public string? Position { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
