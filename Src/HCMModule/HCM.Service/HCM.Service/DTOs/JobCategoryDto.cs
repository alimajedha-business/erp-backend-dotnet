using System.ComponentModel.DataAnnotations;

namespace NGErp.HCM.Service.DTOs;

public record JobCategoryDto(
     Guid Id,
     int Code,
     string Title
);

public record CreateJobCategoryDto(
    int Code,
    string Title
    );

public record PatchJobCategoryDto(
    int? Code,
    string? Title
    );