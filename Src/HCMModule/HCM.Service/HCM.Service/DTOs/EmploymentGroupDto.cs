using System.Text.Json.Serialization;

namespace NGErp.HCM.Service.DTOs;

public record EmploymentGroupDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}

public record EmploymentGroupDetailDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public IReadOnlyList<EmploymentGroupSpecificationDto> Specifications { get; init; } = new
        List<EmploymentGroupSpecificationDto>();
}

public record CreateEmploymentGroupDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public IReadOnlyList<CreateEmploymentGroupSpecificationDto> ;
}