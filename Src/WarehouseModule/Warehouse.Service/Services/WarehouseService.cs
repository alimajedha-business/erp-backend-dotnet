using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseRepository warehouseRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IWarehouseService
{
    private readonly string _key = "Warehouse";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;

    public async Task<WarehouseDto> CreateAsync(
        Guid companyId,
        CreateWarehouseDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<Domain.Entities.Warehouse>(createDto);
        entity.CompanyId = companyId;

        var created = await _warehouseRepository.AddAsync(entity, ct);

        await _warehouseRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseDto>(created);
    }

    public async Task<WarehouseDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var warehouse = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            ct
        );

        return _mapper.Map<WarehouseDto>(warehouse);
    }

    public async Task<ListResponseModel<WarehouseSlimDto>> FilterByQAsync(
        Guid companyId,
        WarehouseParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _warehouseRepository.FilterByQ(companyId, parameters);
        var res = await _warehouseRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseSlimDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseListDto>> GetFilteredAsync(
        Guid companyId,
        WarehouseParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Domain.Entities.Warehouse>(filterNodeDto);
        var query = _warehouseRepository.GetFiltered(companyId, advancedFilters);
        var res = await _warehouseRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseListDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<WarehouseDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchWarehouseDto> patchDocument,
        CancellationToken ct
    )
    {
        var warehouse = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchWarehouseDto>(warehouse);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, warehouse);

        await _warehouseRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseDto>(warehouse);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _warehouseRepository.Remove(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );
    }

    public Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _warehouseRepository.GetNextCodeAsync(companyId, ct);
    }

    private async Task<Domain.Entities.Warehouse> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<Domain.Entities.Warehouse, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _warehouseRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
