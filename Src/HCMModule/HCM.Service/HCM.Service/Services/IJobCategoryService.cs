using System.Linq.Expressions;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IJobCategoryService
{
    Task<JobCategoryDto> CreateAsync(
        CreateJobCategoryDto createDto,
        CancellationToken ct
    );

    Task<JobCategoryDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<JobCategoryDto>> FilterByQAsync(
        JobCategoryParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<JobCategoryDto>> GetFilteredAsync(
        JobCategoryParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<JobCategoryDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchJobCategoryDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );

    Task<JobCategory> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<JobCategory, bool>> predicate,
        CancellationToken ct = default
    );
}