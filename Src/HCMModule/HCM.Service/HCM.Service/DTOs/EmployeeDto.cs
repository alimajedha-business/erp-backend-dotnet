using System.ComponentModel.DataAnnotations;
using NGErp.General.Service.DTOs;
namespace NGErp.HCM.Service.DTOs;

public record EmployeeDto(
    Guid Id,
    Guid PersonId,
    string? Code,
    string? CaseNumber,
    Guid? MilitaryServiceStatusId,
    MilitaryServiceStatusDto? MilitaryServiceStatus,
    Guid MaritalStatusId,
    MaritalStatusDto MaritalStatus,
    DateTime? MarriageDate
);

public record EmployeeListDto(
    Guid Id,
    Guid PersonId,
    string? Code,
    string? CaseNumber,
    string? MilitaryServiceStatusTitle,
    string MaritalStatusTitle,
    DateTime? MarriageDate
);


public class CreateEmployeeDto
{
    [Required]
    public Guid? PersonId { get; set; }
    public string? Code { get; set; }
    public string? CaseNumber { get; set; }
    public Guid? MilitaryServiceStatusId { get; set; }
    [Required]
    public Guid? MaritalStatusId { get; set; }
    public DateTime? MarriageDate { get; set; }
}

public class PatchEmployeeDto
{
    public Guid? PersonId { get; set; }
    public string? Code { get; set; }
    public string? CaseNumber { get; set; }
    public Guid? MilitaryServiceStatusId { get; set; }
    public Guid? MaritalStatusId { get; set; }
    public DateTime? MarriageDate { get; set; }
}

