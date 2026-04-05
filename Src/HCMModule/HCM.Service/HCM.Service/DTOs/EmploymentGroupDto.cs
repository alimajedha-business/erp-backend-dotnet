namespace NGErp.HCM.Service.DTOs;

public class EmploymentGroupDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<EmploymentGroupSpecificationDto>? Specifications { get; set; }
}