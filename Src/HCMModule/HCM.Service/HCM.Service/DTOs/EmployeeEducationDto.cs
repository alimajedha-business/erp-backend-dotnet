using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeEducationDto
{
    public Guid Id { get; set; }
    public EmployeeBaseDto? Employee { get; set; }
    public EducationLevelDto? EducationLevel { get; set; }
    public EducationFieldDto? EducationField { get; set; }
    [MaxLength(50)]
    public string? MajoringCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    [MaxLength(50)]
    public string? CenterCode { get; set; }
    public DateTime EffectiveDate { get; set; }
    public bool IsDefault { get; set; }
}

public class CreateEmployeeEducationDto
{
    [Required]
    public Guid? EducationLevelId { get; set; }
    public Guid? EducationFieldId { get; set; }
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
    public bool IsDefault { get; set; }
}

public class PatchEmployeeEducationDto
{
    public Guid? EducationLevelId { get; set; }
    public Guid? EducationFieldId { get; set; }
    [MaxLength(50)]
    public string? MajoringCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? GPA { get; set; }
    [MaxLength(50)]
    public string? CenterCode { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public bool? IsDefault { get; set; }
}
