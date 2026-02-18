using System.Xml.XPath;

using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;
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
    ) : BaseServiceWithCompany<
        Position,
        PositionDto,
        PositionParameters,
        IPositionRepository,
        HCMResource
        >(
        filterBuilder,
        positionRepository,
        companyService,
        mapper,
        localizer
        ),
    IPositionService
{
    protected override string LocalizerKey => "Position";
    private readonly IPositionRepository _positionRepository = positionRepository;

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool newStatus,
        CancellationToken ct)
    {
        await EnsureCompanyAsync(companyId, ct);
        var position = await GetByIdOrThrowAsync(companyId, id, ct);

        position.ChangeStatus(newStatus, DateTime.UtcNow);

        _positionRepository.Update(position);
        await _positionRepository.SaveChangesAsync(ct);
    }

    public Task<PositionDto> CreatePositionAsync(
        Guid companyId,
        CreatePositionDto createDto,
        CancellationToken ct
        ) => CreateAsync(companyId, createDto, ct);

    public Task DeletePositionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        ) => DeleteAsync(companyId, id, ct);

    public Task<ListResponseModel<PositionDto>> GetAllPositionsAsync(
        Guid companyId,
        PositionParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
        ) => GetAllAsync(companyId, parameters, ct, filterNodeDto);

    public Task<PositionDto> GetPositionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        ) => GetByIdAsync(companyId, id, ct);

    public Task<PositionDto> PatchPositionAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchPositionDto> patchDocument,
        CancellationToken ct
        ) => PatchAsync(companyId, id, patchDocument, ct);
}