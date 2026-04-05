using System.ComponentModel.DataAnnotations;

using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public class EmploymentGroupSpecificationDto
{
    public Guid Id { get; set; }
    public required MonthTypeEnum MonthType { get; set; }
    public required int WorkMinutes { get; set; }

    [StringLength(7)]
    public required string ValidFrom { get; set; }

    [StringLength(7)]
    public string? ValidTo { get; set; }
}