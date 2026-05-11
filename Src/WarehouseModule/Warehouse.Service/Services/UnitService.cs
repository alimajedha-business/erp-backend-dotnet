using System.Linq.Expressions;

using AutoMapper;

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

public class UnitService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitRepository unitRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IUnitService
{
    private readonly string _key = "Unit";

    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IUnitRepository _unitRepository = unitRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<UnitDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var unit = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.Id == id,
            ct
        );

        return _mapper.Map<UnitDto>(unit);
    }

    public async Task<ListResponseModel<UnitSlimDto>> FilterByQAsync(
        UnitParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _unitRepository.FilterByQ(parameters);
        var res = await _unitRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<UnitSlimDto>(
            results: _mapper.Map<IReadOnlyList<UnitSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<UnitDto>> GetFilteredAsync(
        UnitParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Unit>(filterNodeDto);
        var query = _unitRepository.GetFiltered(advancedFilters);
        var res = await _unitRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<UnitDto>(
            results: _mapper.Map<IReadOnlyList<UnitDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    private async Task<Unit> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<Unit, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _unitRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
