using System.ComponentModel.DataAnnotations;

using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public enum SpecificationOperationType
{
    Create = 1,
    Delete = 2
}

public record EmploymentGroupSpecificationDto
{
    public Guid Id { get; init; }
    public MonthType MonthType { get; init; }
    public int WorkMinutes { get; init; }
    public DateOnly ValidFrom { get; init; }
    public DateOnly? ValidTo { get; init; }
}

public record CreateEmploymentGroupSpecificationDto
{
    public required MonthType MonthType { get; init; }
    public int WorkMinutes { get; init; }
    public DateOnly ValidFrom { get; init; }
}

public record UpdateEmploymentGroupSpecificationDto
{
    public Guid? Id { get; init; }
    public required MonthType MonthType { get; init; }
    public int WorkMinutes { get; init; }
    public DateOnly ValidFrom { get; init; }
    public SpecificationOperationType? OperationType { get; init; }
}