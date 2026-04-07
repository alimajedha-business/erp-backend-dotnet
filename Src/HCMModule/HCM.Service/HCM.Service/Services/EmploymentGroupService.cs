using AutoMapper;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

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
    IValidator<EmploymentGroup> validator,
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
        validator,
        localizer
        ),
    IEmploymentGroupService
{
    protected override string LocalizerKey => "EmploymentGroup";

    async Task<EmploymentGroupWithSpecificationDto> IEmploymentGroupService.GetByIdAsync(
         Guid companyId,
         Guid id,
         CancellationToken ct
         )
    {
        await EnsureCompanyAsync(companyId, ct);
        var employmentGroup = await _repo.Find(companyId, e => e.Id == id)
            .AsNoTracking()
            .AsSingleQuery()
            .Include(e => e.Specifications)
            .FirstOrDefaultAsync();

        throw new NotImplementedException();
    }
}