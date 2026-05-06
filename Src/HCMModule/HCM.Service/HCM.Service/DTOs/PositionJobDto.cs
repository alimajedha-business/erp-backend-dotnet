using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class PositionJobDto
{
    public Guid Id { get; set; }
    public Guid PositionId { get; set; }
    public PositionDto? Position { get; set; }
    public Guid JobId { get; set; }
    public JobDto? Job { get; set; }
}

public class CreatePositionJobDto
{
    [Required]
    public Guid? PositionId { get; set; }
    [Required]
    public Guid? JobId { get; set; }
}

public class PatchPositionJobDto
{
    public Guid? PositionId { get; set; }
    public Guid? JobId { get; set; }
}
