using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.HCM.Service.DTOs;

public class PositionDto
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; } = true;
    public DateTime? StatusChangeDate { get; set; }
}

public class CreatePositionDto
{
    public string? Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class PatchPositionDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class PositionChangeStatusDto
{
    public bool Status { get; set; }
}