using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public class JobDto
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string Code { get; set; } = default!;
    [MaxLength(500)]
    [MinLength(2)]
    public string Title { get; set; } = default!;
    public Guid JobCategoryId { get; set; }
    public JobCategoryDto? JobCategory { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    public int LevelCode { get; set; }
}

public class CreateJobDto
{
    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = default!;
    [Required]
    [MaxLength(500)]
    [MinLength(2)]
    public string Title { get; set; } = default!;
    [Required]
    public Guid? JobCategoryId { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    [Required]
    public int? LevelCode { get; set; }
}

public class PatchJobDto
{
    [MaxLength(50)]
    public string? Code { get; set; }
    [MaxLength(500)]
    [MinLength(2)]
    public string? Title { get; set; }
    public Guid? JobCategoryId { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    public int? LevelCode { get; set; }
}
