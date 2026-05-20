using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IRemittanceFieldDefinitionService
{
    Task<RemittanceFieldDefinitionDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<RemittanceFieldDefinitionListDto>> GetFilteredAsync(
        Guid companyId,
        RemittanceFieldDefinitionParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );
}
