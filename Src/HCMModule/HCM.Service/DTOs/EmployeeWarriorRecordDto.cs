using System.ComponentModel.DataAnnotations;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeWarriorRecordDto
{
    public Guid Id { get; set; }
    public EmployeeBaseDto? Employee { get; set; }
    [MaxLength(50)]
    public string Code { get; set; } = default!;
    [Range(0, 100)]
    public decimal? DisabilityPercentage { get; set; }
    public int? IncentiveGroup { get; set; }
    public decimal? Score { get; set; }
    public DateTime EffectiveDate { get; set; }
    [Range(1, 2)]
    [EnumDataType(typeof(VeteranServiceType))]
    public int VeteranServiceType { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
}

public class CreateEmployeeWarriorRecordDto
{
    [Required]
    [MaxLength(50)]
    public required string Code { get; set; }
    [Range(0, 100)]
    public decimal? DisabilityPercentage { get; set; }
    public int? IncentiveGroup { get; set; }
    public decimal? Score { get; set; }
    [Required]
    public DateTime? EffectiveDate { get; set; }
    [Required]
    [Range(1, 2)]
    [EnumDataType(typeof(VeteranServiceType))]
    public int? VeteranServiceType { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
}

public class PatchEmployeeWarriorRecordDto
{
    [MaxLength(50)]
    public string? Code { get; set; }
    [Range(0, 100)]
    public decimal? DisabilityPercentage { get; set; }
    public int? IncentiveGroup { get; set; }
    public decimal? Score { get; set; }
    public DateTime? EffectiveDate { get; set; }
    [Range(1, 2)]
    [EnumDataType(typeof(VeteranServiceType))]
    public int? VeteranServiceType { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
}
