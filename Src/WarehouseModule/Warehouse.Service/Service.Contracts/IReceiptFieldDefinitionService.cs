using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptFieldDefinitionService
{
    Task<ReceiptFieldDefinitionDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptFieldDefinitionSlimDto>> FilterByQAsync(
        Guid companyId,
        ReceiptFieldDefinitionParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptFieldDefinitionDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptFieldDefinitionParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );
}
