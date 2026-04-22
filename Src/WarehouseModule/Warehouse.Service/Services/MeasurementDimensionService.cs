using System.Linq.Expressions;

using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class MeasurementDimensionService(
    IMeasurementDimensionRepository dimensionRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IMeasurementDimensionService
{
    private readonly string _key = "MeasurementDimension";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IMeasurementDimensionRepository _dimensionRepository = dimensionRepository;

    public async Task<MeasurementDimensionDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var measurementDimension = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.Id == id,
            ct
        );

        return _mapper.Map<MeasurementDimensionDto>(measurementDimension);
    }

    public async Task<ListResponseModel<MeasurementDimensionSlimDto>> FilterByQAsync(
        MeasurementDimensionParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _dimensionRepository.FilterByQ(parameters);
        var res = await _dimensionRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<MeasurementDimensionSlimDto>(
            results: _mapper.Map<IReadOnlyList<MeasurementDimensionSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    private async Task<MeasurementDimension> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<MeasurementDimension, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _dimensionRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}

