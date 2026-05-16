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

public class SiUnitService(
    IAdvancedFilterBuilder filterBuilder,
    ISiUnitRepository unitRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : ISiUnitService
{
    private readonly string _key = "SiUnit";

    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ISiUnitRepository _unitRepository = unitRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<SiUnitDto> GetByIdAsync(
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

        return Localize(_mapper.Map<SiUnitDto>(unit));
    }

    public async Task<ListResponseModel<SiUnitSlimDto>> FilterByQAsync(
        SiUnitParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _unitRepository.FilterByQ(parameters);
        var res = await _unitRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<SiUnitSlimDto>(
            results: [.. _mapper
                .Map<IReadOnlyList<SiUnitSlimDto>>(res.items)
                .Select(Localize)],
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<SiUnitDto>> GetFilteredAsync(
        SiUnitParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<SiUnit>(filterNodeDto);
        var query = _unitRepository.GetFiltered(advancedFilters);
        var res = await _unitRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<SiUnitDto>(
            results: [.. _mapper
                .Map<IReadOnlyList<SiUnitDto>>(res.items)
                .Select(Localize)],
            totalCount: res.count,
            parameters
        );
    }

    private async Task<SiUnit> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<SiUnit, bool>> predicate,
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

    private SiUnitDto Localize(SiUnitDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }

    private SiUnitSlimDto Localize(SiUnitSlimDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }
}
