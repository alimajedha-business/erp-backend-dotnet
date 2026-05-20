using System.Linq.Expressions;

using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class RemittanceFieldDefinitionService(
    IAdvancedFilterBuilder filterBuilder,
    IRemittanceFieldDefinitionRepository fieldDefinitionRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IRemittanceFieldDefinitionService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IRemittanceFieldDefinitionRepository _fieldDefinitionRepository =
        fieldDefinitionRepository;

    public async Task<RemittanceFieldDefinitionDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var fieldDefinition = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        return Localize(_mapper.Map<RemittanceFieldDefinitionDto>(fieldDefinition));
    }

    public async Task<ListResponseModel<RemittanceFieldDefinitionListDto>> GetFilteredAsync(
        Guid companyId,
        RemittanceFieldDefinitionParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<RemittanceFieldDefinition>(filterNodeDto);
        var query = _fieldDefinitionRepository
            .GetFiltered(companyId, advancedFilters)
            .Where(e => e.IsActive == true);

        var res = await _fieldDefinitionRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<RemittanceFieldDefinitionListDto>(
            results: [.. _mapper
                .Map<IReadOnlyList<RemittanceFieldDefinitionListDto>>(res.items)
                .Select(Localize)],
            totalCount: res.count,
            parameters
        );
    }

    private async Task<RemittanceFieldDefinition> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<RemittanceFieldDefinition, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _fieldDefinitionRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new RemittanceFieldDefinitionNotFoundException();
    }

    private RemittanceFieldDefinitionDto Localize(RemittanceFieldDefinitionDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }

    private RemittanceFieldDefinitionListDto Localize(RemittanceFieldDefinitionListDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }
}
