using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptFieldValueService
{
    Task<ListResponseModel<ReceiptFieldValueReferenceDto>> FilterByQAsync(
        Guid companyId,
        ReceiptReferenceEntityType reference,
        RequestParameters parameters,
        CancellationToken ct = default
    );
}
