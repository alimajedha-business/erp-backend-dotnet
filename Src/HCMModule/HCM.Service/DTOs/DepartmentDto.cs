using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; } = true;
    public DateTime? StatusChangeDate { get; set; }
}

public class CreateDepartmentDto
{
    public string? Code { get; set; }

    [MinLength(2)]
    public required string Name { get; set; }

    public string? Description { get; set; }
}

public class PatchDepartmentDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class DepartmentChangeStatusDto
{
    public bool Status { get; set; }
    public DateOnly? Date { get; set; }
}