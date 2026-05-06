using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IPositionJobService
{
    Task<PositionJobDto> CreateAsync(
        CreatePositionJobDto createDto,
        CancellationToken ct
    );

    Task<PositionJobDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<PositionJobDto>> GetFilteredAsync(
        PositionJobParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<PositionJobDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchPositionJobDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
