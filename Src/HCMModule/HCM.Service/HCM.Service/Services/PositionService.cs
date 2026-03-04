using AutoMapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Resources;
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
    IPythonIntegrationService integrationService,
    IStringLocalizer<GeneralResource> generalLocalizer,
    IAdvancedFilterBuilder filterBuilder
    ) : IPositionService
{
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;
    private readonly IStringLocalizer<GeneralResource> _generalLocalizer = generalLocalizer;
    private readonly IPythonIntegrationService _integrationService = integrationService;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;

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
        FilterNodeDto? filterNodeDto =  null
        )
    {
        var advancedFilters = _filterBuilder.Build<Position>(filterNodeDto);
        var listQueryResult = await _positionRepository.GetAllAsync(
          companyId,
          positionParameters,
          ct,
          advancedFilters
          );

        return new ListResponseModel<PositionDto>(
            results: _mapper.Map<IReadOnlyList<PositionDto>>(listQueryResult.items),
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

    public Task<PositionDto> UpdatePositionAsync(
        Guid companyId,
        Guid id,
        UpdatePositionDto dto
        )
    {
        throw new NotImplementedException();
    }

    private async Task<Position> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
        )
    {
        var position = await _positionRepository.GetByIdAsync(companyId, id, ct, trackChanges);
        return position ?? throw new NotFoundException(_localizer["Position"].Value);
    }
}
