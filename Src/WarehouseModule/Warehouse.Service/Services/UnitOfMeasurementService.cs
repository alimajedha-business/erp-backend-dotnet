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
        var uom = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.Id == id,
            ct
        );

        return _mapper.Map<UnitOfMeasurementDto>(uom);
    }

    public async Task<ListResponseModel<UnitOfMeasurementSlimDto>> FilterByQAsync(
        UnitOfMeasurementParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _uomRepository.FilterByQ(parameters);
        var res = await _uomRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<UnitOfMeasurementSlimDto>(
            results: _mapper.Map<IReadOnlyList<UnitOfMeasurementSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<UnitOfMeasurementDto>> GetFilteredAsync(
        UnitOfMeasurementParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<UnitOfMeasurement>(filterNodeDto);
        var query = _uomRepository.GetFiltered(advancedFilters);
        var res = await _uomRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<UnitOfMeasurementDto>(
            results: _mapper.Map<IReadOnlyList<UnitOfMeasurementDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<UnitOfMeasurementDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementDto> patchDocument,
        CancellationToken ct
    )
    {
        var uom = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchUnitOfMeasurementDto>(uom);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, uom);

        await _uomRepository.SaveChangesAsync(ct);
        return _mapper.Map<UnitOfMeasurementDto>(uom);
    }

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        await _uomRepository.Remove(e => e.Id == id, ct);
    }

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _uomRepository.GetNextCodeAsync(ct);
    }

    private async Task<UnitOfMeasurement> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<UnitOfMeasurement, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _uomRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}


