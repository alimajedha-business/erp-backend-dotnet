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

public class FeatureSettingsService(
    IAdvancedFilterBuilder filterBuilder,
    IFeatureSettingsRepository featureSettingsRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IFeatureSettingsService
{
    private readonly string _key = "FeatureSettings";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IFeatureSettingsRepository _featureSettingsRepository = featureSettingsRepository;


    public async Task<FeatureSettingsDto> CreateAsync(
        Guid companyId,
        CreateFeatureSettingsDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<FeatureSettings>(createDto);
        entity.CompanyId = companyId;

        var created = await _featureSettingsRepository.AddAsync(entity, ct);

        await _featureSettingsRepository.SaveChangesAsync(ct);
        return _mapper.Map<FeatureSettingsDto>(created);
    }

    public async Task<FeatureSettingsDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<FeatureSettingsDto>(entity);
    }

    public async Task<ListResponseModel<FeatureSettingsDto>> GetFilteredAsync(
        Guid companyId,
        FeatureSettingsParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<FeatureSettings>(filterNodeDto);
        var query = _featureSettingsRepository.GetFiltered(companyId, advancedFilters);
        var res = await _featureSettingsRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<FeatureSettingsDto>(
            results: _mapper.Map<IReadOnlyList<FeatureSettingsDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<FeatureSettingsDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchFeatureSettingsDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchFeatureSettingsDto>(entity);
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

        await _featureSettingsRepository.SaveChangesAsync(ct);
        return _mapper.Map<FeatureSettingsDto>(entity);
    }

    public async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        _featureSettingsRepository.Remove(entity);
        await _featureSettingsRepository.SaveChangesAsync(ct);
    }

    private async Task<FeatureSettings> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _featureSettingsRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
