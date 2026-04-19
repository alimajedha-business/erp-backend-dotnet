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

namespace NGErp.HCM.Service.Services;

public class PositionService(
    IPositionRepository positionRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IPositionService
{
    private readonly string _key = "Position";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IPositionRepository _positionRepository = positionRepository;

    public async Task<PositionDto> CreateAsync(
        Guid companyId,
        CreatePositionDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<Position>(createDto);
        entity.CompanyId = companyId;

        var created = await _positionRepository.AddAsync(entity, ct);

        await _positionRepository.SaveChangesAsync(ct);
        return _mapper.Map<PositionDto>(created);
    }

    public async Task<PositionDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<PositionDto>(entity);
    }

    public async Task<ListResponseModel<PositionDto>> GetFilteredAsync(
        Guid companyId,
        PositionParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Position>(filterNodeDto);
        var query = _positionRepository.GetFiltered(companyId, advancedFilters);
        var res = await _positionRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<PositionDto>(
            results: _mapper.Map<IReadOnlyList<PositionDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<PositionDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchPositionDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchPositionDto>(entity);
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

        await _positionRepository.SaveChangesAsync(ct);
        return _mapper.Map<PositionDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        _positionRepository.Remove(entity);
        await _positionRepository.SaveChangesAsync(ct);
    }

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        PositionChangeStatusDto changeStatusDto,
        CancellationToken ct)
    {
        // TODO: ensure company
        var position = await GetByIdOrThrowAsync(companyId, id, trackChanges: true, ct);

        if (changeStatusDto.Date is null)
            throw new ArgumentException("Date is required.");

        position.ChangeStatus(changeStatusDto.Status,
            new DateTime((DateOnly)changeStatusDto.Date, TimeOnly.MinValue)
        );

        _positionRepository.Update(position);
        await _positionRepository.SaveChangesAsync(ct);
    }

    private async Task<Position> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        // TODO: add specification if needed
        var entity = await _positionRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}