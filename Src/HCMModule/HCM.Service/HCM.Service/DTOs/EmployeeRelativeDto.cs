using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeRelativeDto
{
    public Guid Id { get; set; }
    public EmployeeBaseDto Employee { get; set; } = default!;
    [MaxLength(100)]
    public string Name { get; set; } = default!;
    [MaxLength(100)]
    public string Family { get; set; } = default!;
    [MaxLength(100)]
    public string? FatherName { get; set; }
    public DateTime? BirthDate { get; set; }
    [MaxLength(100)]
    public string? BirthPlace { get; set; }
    public Guid? MaritalStatusId { get; set; }
    public MaritalStatusDto? MaritalStatus { get; set; }
    [MaxLength(10)]
    public string? NationalCode { get; set; }
    [MaxLength(50)]
    public string? IdNumber { get; set; }
    public Guid RelativeTypeId { get; set; }
    public RelativeTypeDto? RelativeType { get; set; }
    [MaxLength(50)]
    public string? LevelCode { get; set; }
    [MaxLength(100)]
    public string? PhysicalCondition { get; set; }
    public Guid? EducationalStatusId { get; set; }
    public EducationalStatusDto? EducationalStatus { get; set; }
}

public class CreateEmployeeRelativeDto
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Family { get; set; }
    [MaxLength(100)]
    public string? FatherName { get; set; }
    public DateTime? BirthDate { get; set; }
    [MaxLength(100)]
    public string? BirthPlace { get; set; }
    public Guid? MaritalStatusId { get; set; }
    [MaxLength(10)]
    public string? NationalCode { get; set; }
    [MaxLength(50)]
    public string? IdNumber { get; set; }
    [Required]
    public Guid? RelativeTypeId { get; set; }
    [MaxLength(50)]
    public string? LevelCode { get; set; }
    [MaxLength(100)]
    public string? PhysicalCondition { get; set; }
    public Guid? EducationalStatusId { get; set; }
}

public class PatchEmployeeRelativeDto
{
    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(100)]
    public string? Family { get; set; }
    [MaxLength(100)]
    public string? FatherName { get; set; }
    public DateTime? BirthDate { get; set; }
    [MaxLength(100)]
    public string? BirthPlace { get; set; }
    public Guid? MaritalStatusId { get; set; }
    [MaxLength(10)]
    public string? NationalCode { get; set; }
    [MaxLength(50)]
    public string? IdNumber { get; set; }
    public Guid? RelativeTypeId { get; set; }
    [MaxLength(50)]
    public string? LevelCode { get; set; }
    [MaxLength(100)]
    public string? PhysicalCondition { get; set; }
    public Guid? EducationalStatusId { get; set; }
}
