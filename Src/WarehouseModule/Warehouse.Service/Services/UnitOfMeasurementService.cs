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

public class UnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementRepository uomRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IUnitOfMeasurementService
{
    private readonly string _key = "UnitOfMeasurement";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IUnitOfMeasurementRepository _uomRepository = uomRepository;

    public async Task<UnitOfMeasurementDto> CreateAsync(
        CreateUnitOfMeasurementDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<UnitOfMeasurement>(createDto);
        var created = await _uomRepository.AddAsync(entity, ct);

        await _uomRepository.SaveChangesAsync(ct);
        return _mapper.Map<UnitOfMeasurementDto>(created);
    }

    public async Task<UnitOfMeasurementDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<UnitOfMeasurementDto>(entity);
    }

    public async Task<ListResponseModel<UnitOfMeasurementDto>> GetAllAsync(
        UnitOfMeasurementParameters parameters,
        CancellationToken ct = default
    )
    {
        var listQueryResult = await _uomRepository.GetAllAsync(
            parameters,
            spec: null,
            ct
        );

        return new ListResponseModel<UnitOfMeasurementDto>(
            results: _mapper.Map<IReadOnlyList<UnitOfMeasurementDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<UnitOfMeasurementDto>> GetAllAsync(
        UnitOfMeasurementParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<UnitOfMeasurement>(filterNodeDto);
        var listQueryResult = await _uomRepository.GetAllAsync(
            parameters,
            advancedFilters,
            spec: null,
            ct
        );

        return new ListResponseModel<UnitOfMeasurementDto>(
            results: _mapper.Map<IReadOnlyList<UnitOfMeasurementDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<UnitOfMeasurementDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchUnitOfMeasurementDto>(entity);
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

        await _uomRepository.SaveChangesAsync(ct);
        return _mapper.Map<UnitOfMeasurementDto>(entity);
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

        _uomRepository.Remove(entity);
        await _uomRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _uomRepository.GetNextCodeAsync(ct);
    }

    private async Task<UnitOfMeasurement> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _uomRepository.GetByIdAsync(
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
