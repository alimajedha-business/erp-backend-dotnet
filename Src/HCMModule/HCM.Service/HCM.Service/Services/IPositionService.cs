using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IPositionService
{
    Task<ListResponseModel<PositionDto>> GetAllPositionsAsync(
        Guid companyId,
        PositionParameters positionParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
        );
    Task<PositionDto?> GetPositionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        );
    Task<PositionDto> CreatePositionAsync(
        Guid companyId,
        CreatePositionDto createDepartmentDto,
        CancellationToken ct
        );
    Task<PositionDto> UpdatePositionAsync(
        Guid companyId,
        Guid id,
        UpdatePositionDto updatePositionDto
        );
    Task<bool> DeletePositionAsync(
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
