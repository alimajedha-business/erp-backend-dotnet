using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IPositionService
{
    Task<ListResponseModel<PositionDto>> GetAllPositionsAsync(
        Guid companyId,
        PositionParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
        );

    Task<PositionDto> GetPositionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        );

    Task<PositionDto> CreatePositionAsync(
        Guid companyId,
        CreatePositionDto createDto,
        CancellationToken ct
        );

    Task<PositionDto> PatchPositionAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchPositionDto> patchDocument,
        CancellationToken ct
        );

    Task DeletePositionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        );

    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool NewStatus,
        CancellationToken ct
        );
}