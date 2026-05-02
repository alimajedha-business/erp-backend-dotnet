using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeEducationDto
{
    public Guid Id { get; set; }
    public EmployeeBaseDto? Employee { get; set; }
    [MaxLength(50)]
    public string? LevelCode { get; set; }
    [MaxLength(50)]
    public string? FieldCode { get; set; }
    [MaxLength(50)]
    public string? MajoringCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    [MaxLength(50)]
    public string? CenterCode { get; set; }
    public DateTime EffectiveDate { get; set; }
}

public class CreateEmployeeEducationDto
{
    [Required]
    [MaxLength(50)]
    public string? LevelCode { get; set; }
    [MaxLength(50)]
    public string? FieldCode { get; set; }
    [MaxLength(50)]
    public string? MajoringCode { get; set; }
    [Required]
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    [MaxLength(50)]
    public string? CenterCode { get; set; }
    [Required]
    public DateTime? EffectiveDate { get; set; }
}

public class PatchEmployeeEducationDto
{
    [MaxLength(50)]
    public string? LevelCode { get; set; }
    [MaxLength(50)]
    public string? FieldCode { get; set; }
    [MaxLength(50)]
    public string? MajoringCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    [MaxLength(50)]
    public string? CenterCode { get; set; }
    public DateTime? EffectiveDate { get; set; }
}
