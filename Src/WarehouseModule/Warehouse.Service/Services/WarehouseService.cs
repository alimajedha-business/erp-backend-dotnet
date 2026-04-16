using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;
using NGErp.Warehouse.Service.Specifications;

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
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            spec: new WarehouseSpecification(),
            ct
        );

        return _mapper.Map<WarehouseDto>(entity);
    }

    public async Task<ListResponseModel<WarehouseListDto>> GetAllAsync(
        Guid companyId,
        WarehouseParameters parameters,
        CancellationToken ct = default
    )
    {
        var listQueryResult = await _warehouseRepository.GetAllAsync(
            companyId,
            parameters,
            spec: new WarehouseListSpecification(),
            ct
        );

        return new ListResponseModel<WarehouseListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseListDto>> GetAllAsync(
        Guid companyId,
        WarehouseParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Domain.Entities.Warehouse>(filterNodeDto);
        var listQueryResult = await _warehouseRepository.GetAllAsync(
            parameters,
            advancedFilters,
            spec: new WarehouseListSpecification(),
            ct
        );

        return new ListResponseModel<WarehouseListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
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
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            spec: new WarehouseSpecification(),
            ct
        );

        var patchDto = _mapper.Map<PatchWarehouseDto>(entity);
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

        await _warehouseRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        // TODO: check permissions, and
        // check if the warehouse belongs to this company
        _warehouseRepository.Remove(id, ct);
        await _warehouseRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _warehouseRepository.GetNextCodeAsync(companyId, ct);
    }

    private async Task<Domain.Entities.Warehouse> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges,
        ISpecification<Domain.Entities.Warehouse>? spec = null,
        CancellationToken ct = default
    )
    {
        var entity = await _warehouseRepository.GetByIdAsync(
            companyId,
            id,
            trackChanges,
            spec,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
