using System.Linq.Expressions;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IReceiptTypeService
{
    Task<ReceiptTypeDto> CreateAsync(
        Guid companyId,
        CreateReceiptTypeDto createDto,
        CancellationToken ct
    );

    Task<ReceiptTypeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptTypeSlimDto>> FilterByQAsync(
        Guid companyId,
        ReceiptTypeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ReceiptTypeDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ReceiptTypeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchReceiptTypeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(Guid companyId, CancellationToken ct);

    Task<ReceiptType> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<ReceiptType, bool>> predicate,
        CancellationToken ct = default
    );
}
