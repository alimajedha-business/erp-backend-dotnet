using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

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

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool newStatus,
        CancellationToken ct)
    {
        await EnsureCompanyAsync(companyId, ct);
        var position = await GetByIdOrThrowAsync(companyId, id, ct);

        position.ChangeStatus(newStatus, DateTime.UtcNow);

        _repo.Update(position);
        await _repo.SaveChangesAsync(ct);
    }
}