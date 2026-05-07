using System.Linq.Expressions;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptTypeConfigurationService
{
    Task<ReceiptTypeConfigurationDto> CreateAsync(
        Guid companyId,
        CreateReceiptTypeConfigurationDto createDto,
        CancellationToken ct
    );

    Task<ReceiptTypeConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ReceiptTypeConfigurationDto> GetByReceiptTypeIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptTypeConfigurationListDto>> FilterByQAsync(
        Guid companyId,
        ReceiptTypeConfigurationParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptTypeConfigurationDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptTypeConfigurationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<ReceiptTypeConfiguration> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<ReceiptTypeConfiguration, bool>> predicate,
        CancellationToken ct = default
    );
}
