using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.DTOs;

public record OrganizationNodeDto
{
    public Guid Id { get; init; }
    public NodeType NodeType { get; init; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public DepartmentDto? Department { get; set; }
    public PositionDto? Position { get; set; }
}

public record OrganizationNodeTreeDto
{
    public Guid Id { get; init; }
    public NodeType NodeType { get; init; }
    public DepartmentDto? Department { get; set; }
    public PositionDto? Position { get; set; }
}