using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class WorkLocationDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? ParentId { get; set; }
    public WorkLocationDto? Parent { get; set; }
    [MaxLength(250)]
    public string Title { get; set; } = default!;
}

public class CreateWorkLocationDto
{
    public Guid? ParentId { get; set; }
    [Required]
    [MaxLength(250)]
    public string Title { get; set; } = default!;
}

public class PatchWorkLocationDto
{
    public Guid? ParentId { get; set; }
    [MaxLength(250)]
    public string? Title { get; set; }
}
