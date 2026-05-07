using System.Linq.Expressions;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

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
