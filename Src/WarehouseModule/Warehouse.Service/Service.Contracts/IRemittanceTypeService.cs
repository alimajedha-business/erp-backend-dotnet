using System.Linq.Expressions;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IRemittanceTypeService
{
    Task<RemittanceTypeDto> CreateAsync(
        Guid companyId,
        CreateRemittanceTypeDto createDto,
        CancellationToken ct
    );

    Task<RemittanceTypeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<RemittanceTypeSlimDto>> FilterByQAsync(
        Guid companyId,
        RemittanceTypeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<RemittanceTypeDto>> GetFilteredAsync(
        Guid companyId,
        RemittanceTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<RemittanceTypeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchRemittanceTypeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(Guid companyId, CancellationToken ct);

    Task<RemittanceType> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<RemittanceType, bool>> predicate,
        CancellationToken ct = default
    );
}
