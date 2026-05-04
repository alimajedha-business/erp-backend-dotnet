using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class EmployeeDependantDto
{
    public Guid Id { get; set; }
    public Guid EmployeeRelativeId { get; set; }
    public EmployeeRelativeDto EmployeeRelative { get; set; } = default!;
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class CreateEmployeeDependantDto
{
    [Required]
    public Guid EmployeeRelativeId { get; set; }
    [Required]
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class PatchEmployeeDependantDto
{
    public Guid? EmployeeRelativeId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
