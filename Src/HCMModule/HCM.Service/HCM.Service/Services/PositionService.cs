using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
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
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService
    ) : IPositionService
{
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICompanyService _companyService = companyService;

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool newStatus,
        CancellationToken ct)
    {
        var position = await GetByIdOrThrowExceptionAsync(companyId, id, ct);

        position.ChangeStatus(newStatus, DateTime.UtcNow);

        _positionRepository.Update(position);
        await _positionRepository.SaveChangesAsync(ct);
    }

    public async Task<PositionDto> CreatePositionAsync(
        Guid companyId,
        CreatePositionDto createPositionDto,
        CancellationToken ct
        )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
            );

        var position = _mapper.Map<Position>(createPositionDto);
        position.CompanyId = companyId;

        var createdPosition = await _positionRepository.AddAsync(position, ct);
        await _positionRepository.SaveChangesAsync(ct);

        return _mapper.Map<PositionDto>(createdPosition);
    }

    public async Task<bool> DeletePositionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct)
    {
        var position = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _positionRepository.Remove(position);

        try
        {
            await _positionRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["Position"].Value);
        }

        return true;
    }

    public async Task<ListResponseModel<PositionDto>> GetAllPositionsAsync(
        Guid companyId,
        PositionParameters positionParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
        )

    {
        await _companyService.GetCompanyByIdAsync(
        companyId,
        ct
        );

        var advancedFilters = _filterBuilder.Build<Department>(filterNodeDto);
        var listQueryResult = await _positionRepository.GetAllAsync(
          companyId,
          positionParameters,
          ct,
          advancedFilters
          );

        return new ListResponseModel<PositionDto>(
            items: _mapper.Map<IReadOnlyList<PositionDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            positionParameters
        );
    }

    public async Task<PositionDto?> GetPositionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        )
    {
        var position = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        return _mapper.Map<PositionDto>(position);
    }

    public async Task<PositionDto> PatchPositionAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchPositionDto> jsonPatch,
        CancellationToken ct
        )
    {
        var position = await GetByIdOrThrowExceptionAsync(
           companyId,
           id,
           ct,
           trackChanges: true
       );

        var patchDto = _mapper.Map<PatchPositionDto>(position);
        var errors = new List<string>();

        jsonPatch.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, position);

        try
        {
            await _positionRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateBadRequestException(ex.Message);
        }

        return _mapper.Map<PositionDto>(position);
    }

    private async Task<Position> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
        )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
            );

        var position = await _positionRepository.GetByIdAsync(companyId, id, ct, trackChanges);
        return position ?? throw new NotFoundException(_localizer["Position"].Value);
    }
}