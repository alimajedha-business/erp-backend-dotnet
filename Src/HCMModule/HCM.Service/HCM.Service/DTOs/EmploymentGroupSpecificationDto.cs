using System.ComponentModel.DataAnnotations;

using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record EmploymentGroupSpecificationDto
{
    public Guid Id { get; init; }
    public MonthTypeEnum MonthType { get; init; }
    public int WorkMinutes { get; init; }
    public DateOnly ValidFrom { get; init; }
    public DateOnly? ValidTo { get; init; }
}

public record CreateEmploymentGroupSpecificationDto
{
    public required MonthTypeEnum MonthType { get; init; }
    public int WorkMinutes { get; init; }
    public DateOnly ValidFrom { get; init; }
}