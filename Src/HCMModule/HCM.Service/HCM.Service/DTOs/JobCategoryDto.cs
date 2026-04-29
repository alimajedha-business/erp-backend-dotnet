using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public record JobCategoryDto(
     Guid Id,
     int Code,
     string Title
);

public record CreateJobCategoryDto(
    [Required]
    [Range(1, int.MaxValue)]
    int Code,
    [Required]
    [MaxLength(200)]
    string Title
    );
public record PatchJobCategoryDto(
    int? Code,
    string? Title
    );