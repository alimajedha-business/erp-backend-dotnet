using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeEducationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
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
    public Guid? EmployeeId { get; set; }
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
    public Guid? EmployeeId { get; set; }
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
