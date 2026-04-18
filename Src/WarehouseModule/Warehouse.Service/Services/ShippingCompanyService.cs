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

public class ShippingCompanyService(
    IAdvancedFilterBuilder filterBuilder,
    IShippingCompanyRepository shippingCompanyRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IShippingCompanyService
{
    private readonly string _key = "ShippingCompany";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IShippingCompanyRepository _shippingCompanyRepository = shippingCompanyRepository;

    public async Task<ShippingCompanyDto> CreateAsync(
        CreateShippingCompanyDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<ShippingCompany>(createDto);
        var created = await _shippingCompanyRepository.AddAsync(entity, ct);

        await _shippingCompanyRepository.SaveChangesAsync(ct);
        return _mapper.Map<ShippingCompanyDto>(created);
    }

    public async Task<ShippingCompanyDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<ShippingCompanyDto>(entity);
    }

    public async Task<ListResponseModel<ShippingCompanyDto>> FilterByQAsync(
        ShippingCompanyParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _shippingCompanyRepository.FilterByQ(parameters);
        var res = await _shippingCompanyRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ShippingCompanyDto>(
            results: _mapper.Map<IReadOnlyList<ShippingCompanyDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ShippingCompanyDto>> GetFilteredAsync(
        ShippingCompanyParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<ShippingCompany>(filterNodeDto);
        var query = _shippingCompanyRepository.GetFiltered(advancedFilters);
        var res = await _shippingCompanyRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ShippingCompanyDto>(
            results: _mapper.Map<IReadOnlyList<ShippingCompanyDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<ShippingCompanyDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchShippingCompanyDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchShippingCompanyDto>(entity);
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

        await _shippingCompanyRepository.SaveChangesAsync(ct);
        return _mapper.Map<ShippingCompanyDto>(entity);
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

        _shippingCompanyRepository.Remove(entity);
        await _shippingCompanyRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _shippingCompanyRepository.GetNextCodeAsync(ct);
    }

    private async Task<ShippingCompany> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _shippingCompanyRepository.GetByIdAsync(
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
