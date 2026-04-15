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

public class WarehouseTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseTypeRepository warehouseTypeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IWarehouseTypeService
{
    private readonly string _key = "WarehouseType";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseTypeRepository _warehouseTypeRepository = warehouseTypeRepository;

    public async Task<WarehouseTypeDto> CreateAsync(
        CreateWarehouseTypeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<WarehouseType>(createDto);
        var created = await _warehouseTypeRepository.AddAsync(entity, ct);

        await _warehouseTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseTypeDto>(created);
    }

    public async Task<WarehouseTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<WarehouseTypeDto>(entity);
    }

    public async Task<ListResponseModel<WarehouseTypeDto>> GetAllAsync(
        WarehouseTypeParameters parameters,
        CancellationToken ct = default
    )
    {
        var listQueryResult = await _warehouseTypeRepository.GetAllAsync(
            parameters,
            spec: null,
            ct
        );

        return new ListResponseModel<WarehouseTypeDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseTypeDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseTypeDto>> GetAllAsync(
        WarehouseTypeParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<WarehouseType>(filterNodeDto);
        var listQueryResult = await _warehouseTypeRepository.GetAllAsync(
            parameters,
            advancedFilters,
            spec: null,
            ct
        );

        return new ListResponseModel<WarehouseTypeDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseTypeDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<WarehouseTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchWarehouseTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchWarehouseTypeDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _warehouseTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseTypeDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: true,
            ct
        );

        _warehouseTypeRepository.Remove(entity);
        await _warehouseTypeRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _warehouseTypeRepository.GetNextCodeAsync(ct);
    }

    private async Task<WarehouseType> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _warehouseTypeRepository.GetByIdAsync(
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
