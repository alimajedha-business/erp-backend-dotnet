using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Service.Contracts;

namespace NGErp.HCM.Service.Services;

public class MilitaryServiceStatusService(
    IMilitaryServiceStatusRepository militaryServiceStatusRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IMilitaryServiceStatusService
{
    private readonly string _key = "MilitaryServiceStatus";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IMilitaryServiceStatusRepository _militaryServiceStatusRepository = militaryServiceStatusRepository;

    public async Task<MilitaryServiceStatusDto> CreateAsync(
        CreateMilitaryServiceStatusDto createDto,
        CancellationToken ct
    )
    {

        var entity = _mapper.Map<MilitaryServiceStatus>(createDto);
        var created = await _militaryServiceStatusRepository.AddAsync(entity, ct);

        await _militaryServiceStatusRepository.SaveChangesAsync(ct);
        return _mapper.Map<MilitaryServiceStatusDto>(created);
    }

    public async Task<MilitaryServiceStatusDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {

        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<MilitaryServiceStatusDto>(entity);
    }

    public async Task<ListResponseModel<MilitaryServiceStatusDto>> GetFilteredAsync(
        MilitaryServiceStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {

        var advancedFilters = _filterBuilder.Build<MilitaryServiceStatus>(filterNodeDto);
        var query = _militaryServiceStatusRepository.GetFiltered(advancedFilters);
        var res = await _militaryServiceStatusRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<MilitaryServiceStatusDto>(
            results: _mapper.Map<IReadOnlyList<MilitaryServiceStatusDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<MilitaryServiceStatusDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchMilitaryServiceStatusDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchMilitaryServiceStatusDto>(entity);
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

        await _militaryServiceStatusRepository.SaveChangesAsync(ct);
        return _mapper.Map<MilitaryServiceStatusDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        _militaryServiceStatusRepository.Remove(entity);
        await _militaryServiceStatusRepository.SaveChangesAsync(ct);
    }

    private async Task<MilitaryServiceStatus> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _militaryServiceStatusRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
