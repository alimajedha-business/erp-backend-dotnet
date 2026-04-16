using AutoMapper;
using AutoMapper.QueryableExtensions;

using DocumentFormat.OpenXml.Spreadsheet;
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

    public async Task<EmploymentGroupDetailDto> CreateAsync(Guid companyId,
        CreateEmploymentGroupDto createDto,
        CancellationToken ct
        )
    {
        await EnsureCompanyAsync(companyId, ct);
        var existing = await _repo.FirstOrDefaultAsync(
            e => e.CompanyId == companyId && e.Name == createDto.Name);
        if (existing != null)
        {
            throw new InvalidOperationException("نام گروه استخدامی در شرکت باید یکتا باشد");
        }

        var employmentGroup = _mapper.Map<EmploymentGroup>(createDto);
        employmentGroup.CompanyId = companyId;

        var specDto = createDto.Specification;

        var specification = new EmploymentGroupSpecification
        {
            MonthType = specDto.MonthType,
            WorkMinutes = specDto.WorkMinutes,
            ValidFrom = specDto.ValidFrom
        };

        employmentGroup.Specifications.Add(specification);

        await _repo.AddAsync(employmentGroup, ct);
        await _repo.SaveChangesAsync(ct);

        return new EmploymentGroupDetailDto
        {
            Name = employmentGroup.Name,
            Specifications = employmentGroup.Specifications
            .Select(s => _mapper.Map<EmploymentGroupSpecificationDto>(s))
            .ToList()
        };
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