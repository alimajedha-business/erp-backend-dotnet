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

public class ReceiptFieldDefinitionService(
    IAdvancedFilterBuilder filterBuilder,
    IReceiptFieldDefinitionRepository fieldDefinitionRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptFieldDefinitionService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IReceiptFieldDefinitionRepository _fieldDefinitionRepository =
        fieldDefinitionRepository;

    public async Task<ReceiptFieldDefinitionDto> GetByIdAsync(
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

        return Localize(_mapper.Map<ReceiptFieldDefinitionDto>(fieldDefinition));
    }

    public async Task<ListResponseModel<ReceiptFieldDefinitionSlimDto>> FilterByQAsync(
        Guid companyId,
        ReceiptFieldDefinitionParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _fieldDefinitionRepository.FilterByQ(companyId, parameters);
        var res = await _fieldDefinitionRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptFieldDefinitionSlimDto>(
            results: _mapper
                .Map<IReadOnlyList<ReceiptFieldDefinitionSlimDto>>(res.items)
                .Select(Localize)
                .ToList(),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ReceiptFieldDefinitionDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptFieldDefinitionParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<ReceiptFieldDefinition>(filterNodeDto);
        var query = _fieldDefinitionRepository.GetFiltered(companyId, advancedFilters);
        var res = await _fieldDefinitionRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptFieldDefinitionDto>(
            results: _mapper
                .Map<IReadOnlyList<ReceiptFieldDefinitionDto>>(res.items)
                .Select(Localize)
                .ToList(),
            totalCount: res.count,
            parameters
        );
    }

    private async Task<ReceiptFieldDefinition> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<ReceiptFieldDefinition, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _fieldDefinitionRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new ReceiptFieldDefinitionNotFoundException();
    }

    private ReceiptFieldDefinitionDto Localize(ReceiptFieldDefinitionDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }

    private ReceiptFieldDefinitionSlimDto Localize(ReceiptFieldDefinitionSlimDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }
}
