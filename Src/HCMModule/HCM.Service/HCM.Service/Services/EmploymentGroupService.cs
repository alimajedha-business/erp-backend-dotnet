using AutoMapper;
using AutoMapper.QueryableExtensions;

using DocumentFormat.OpenXml.Vml.Office;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class EmploymentGroupService(
    IEmploymentGroupRepository employmentGroupRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService
    ) : BaseServiceWithCompany<
        EmploymentGroup,
        EmploymentGroupDto,
        EmploymentGroupParameters,
        IEmploymentGroupRepository,
        HCMResource>
    (
        filterBuilder,
        employmentGroupRepository,
        companyService,
        mapper,
        localizer
        ),
    IEmploymentGroupService
{
    protected override string LocalizerKey => "EmploymentGroup";

    public Task<EmploymentGroupDetailDto> CreateAsync(Guid companyId,
        CreateEmploymentGroupDto createDto,
        CancellationToken ct
        )
    {
        throw new NotImplementedException();
    }

    async Task<EmploymentGroupDetailDto?> IEmploymentGroupService.GetByIdAsync(
         Guid companyId,
         Guid id,
         CancellationToken ct
         )
    {
        await EnsureCompanyAsync(companyId, ct);
        var entity = await _repo
       .Find(companyId, e => e.Id == id)
       .AsNoTracking()
       .ProjectTo<EmploymentGroupDetailDto>(_mapper.ConfigurationProvider)
       .FirstOrDefaultAsync(ct);

        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }
}