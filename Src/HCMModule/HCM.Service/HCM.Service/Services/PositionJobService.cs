using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class PositionJobService(
    IPositionJobRepository positionJobRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IPositionJobService
{
    private readonly string _key = "PositionJob";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IPositionJobRepository _positionJobRepository = positionJobRepository;


    public async Task<PositionJobDto> CreateAsync(
        CreatePositionJobDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<PositionJob>(createDto);

        var created = await _positionJobRepository.AddAsync(entity, ct);

        await _positionJobRepository.SaveChangesAsync(ct);
        return _mapper.Map<PositionJobDto>(created);
    }

    public async Task<PositionJobDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<PositionJobDto>(entity);
    }

    public async Task<ListResponseModel<PositionJobDto>> GetFilteredAsync(
        PositionJobParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<PositionJob>(filterNodeDto);
        var query = _positionJobRepository.GetFiltered(advancedFilters);
        var res = await _positionJobRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<PositionJobDto>(
            results: _mapper.Map<IReadOnlyList<PositionJobDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<PositionJobDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchPositionJobDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchPositionJobDto>(entity);
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

        await _positionJobRepository.SaveChangesAsync(ct);
        return _mapper.Map<PositionJobDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: true,
            ct
        );

        _positionJobRepository.Remove(entity);
        await _positionJobRepository.SaveChangesAsync(ct);
    }

    private async Task<PositionJob> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _positionJobRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
