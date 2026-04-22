using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class InventoryMovementTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IInventoryMovementTypeRepository movementTypeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IInventoryMovementTypeService
{
    private readonly string _key = "InventoryMovementType";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IInventoryMovementTypeRepository _movementTypeRepository = movementTypeRepository;

    public async Task<InventoryMovementTypeDto> CreateAsync(
        Guid companyId,
        CreateInventoryMovementTypeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<InventoryMovementType>(createDto);
        entity.CompanyId = companyId;

        var created = await _movementTypeRepository.AddAsync(entity, ct);

        await _movementTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<InventoryMovementTypeDto>(created);
    }

    public async Task<InventoryMovementTypeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var movementType = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.Id == id,
            ct
        );

        return _mapper.Map<InventoryMovementTypeDto>(movementType);
    }

    public async Task<ListResponseModel<InventoryMovementTypeDto>> FilterByQAsync(
        Guid companyId,
        InventoryMovementTypeParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _movementTypeRepository.FilterByQ(companyId, parameters);
        var res = await _movementTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<InventoryMovementTypeDto>(
            results: _mapper.Map<IReadOnlyList<InventoryMovementTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<InventoryMovementTypeDto>> GetFilteredAsync(
        Guid companyId,
        InventoryMovementTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<InventoryMovementType>(filterNodeDto);
        var query = _movementTypeRepository.GetFiltered(companyId, advancedFilters);
        var res = await _movementTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<InventoryMovementTypeDto>(
            results: _mapper.Map<IReadOnlyList<InventoryMovementTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<InventoryMovementTypeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchInventoryMovementTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var movementType = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchInventoryMovementTypeDto>(movementType);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, movementType);

        await _movementTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<InventoryMovementTypeDto>(movementType);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _movementTypeRepository.Remove(e => e.Id == id, ct);
    }

    public Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _movementTypeRepository.GetNextCodeAsync(companyId, ct);
    }

    private async Task<InventoryMovementType> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<InventoryMovementType, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _movementTypeRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
