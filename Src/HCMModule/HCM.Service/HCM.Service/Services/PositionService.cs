using AutoMapper;

using Microsoft.Extensions.Localization;

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
        PositionChangeStatusDto changeStatusDto,
        CancellationToken ct)
    {
        await EnsureCompanyAsync(companyId, ct);
        var position = await GetByIdOrThrowAsync(companyId, id, ct);

        DateTime? dateTime = null;
        if (changeStatusDto.Date.HasValue)
            dateTime = new DateTime((DateOnly)changeStatusDto.Date, TimeOnly.MinValue);
        position.ChangeStatus(changeStatusDto.Status, dateTime);
        _repo.Update(position);
        await _repo.SaveChangesAsync(ct);
    }
}