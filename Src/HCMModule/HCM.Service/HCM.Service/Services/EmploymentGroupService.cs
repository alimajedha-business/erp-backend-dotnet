using AutoMapper;
using AutoMapper.QueryableExtensions;

using Azure.Identity;

using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;
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

    public async Task<EmploymentGroupDetailDto> CreateAsync(
        Guid companyId,
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

    public async Task<EmploymentGroupDetailDto> UpdateAsync(
        Guid companyId,
        Guid id,
        UpdateEmploymentGroupDto updateDto,
        CancellationToken ct)
    {
        await EnsureCompanyAsync(companyId, ct);

        // 1. Load group with all specifications (aggregate root)

        var entity = await _repo.GetWithSpecificationsAsync(companyId, id, ct);

        if (entity == null)
            throw new NotFoundException("Employment group not found.");

        // 2. Check name uniqueness
        var nameExists = await _repo.AnyAsync(e => e.CompanyId == companyId
        && e.Name == updateDto.Name
        && e.Id != id
        );

        if (nameExists)
            throw new NotImplementedException("نام گروه استخدامی در شرکت باید یکتا باشد");

        // 3. Update root values
        entity.Name = updateDto.Name;

        // 4. Process specifications based on OperationEnum
        foreach (var specDto in updateDto.Specifications.Where(s => s.Operation == OperationEnum.Delete))
        {
            HandleDeleteSpecification(entity, specDto);
        }

        foreach (var specDto in updateDto.Specifications.Where(s => s.Operation == OperationEnum.Create))
        {
            HandleCreateSpecification(entity, specDto);
        }

        // 5. Save changes once

        await _repo.SaveChangesAsync(ct);

        // 6. Map and return updated result

        return new EmploymentGroupDetailDto
        {
            Name = entity.Name,
            Specifications = entity.Specifications
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
        // var entity = await _repo
        //.Find(companyId, e => e.Id == id)
        //.AsNoTracking()
        //.ProjectTo<EmploymentGroupDetailDto>(_mapper.ConfigurationProvider)
        //.FirstOrDefaultAsync(ct);

        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }

    private void HandleCreateSpecification(
        EmploymentGroup entity,
        UpdateEmploymentGroupSpecificationDto dto)
    {
        // 1) Sort existing specs by ValidFrom
        var orderedSpecs = entity.Specifications
            .OrderBy(s => s.ValidFrom)
            .ToList();

        // 2) Find previous spec (the one immediately BEFORE new.ValidFrom)
        var previousSpec = orderedSpecs
            .LastOrDefault(s => s.ValidFrom < dto.ValidFrom);

        // 3) If rules say new spec must follow previous one
        // Set previous.ValidTo = new.ValidFrom - 1 day
        if (previousSpec != null)
        {
            previousSpec.ValidTo = dto.ValidFrom.AddDays(-1);
        }

        // 4) Create the new specification
        var newSpec = new EmploymentGroupSpecification

        {
            MonthType = dto.MonthType,
            WorkMinutes = dto.WorkMinutes,
            ValidFrom = dto.ValidFrom,
            ValidTo = null // by definition new spec is active until replaced
        };
        entity.Specifications.Add(newSpec);
    }

    private void HandleDeleteSpecification(
        EmploymentGroup entity,
        UpdateEmploymentGroupSpecificationDto dto
        )
    {
        // TODO:check EmploymentGroup in detail entities and if has duration overlap prevent to delete
        var existing = entity.Specifications.FirstOrDefault(x =>
        x.MonthType == dto.MonthType &&
        x.ValidFrom == dto.ValidFrom);
        if (existing == null)
            throw new NotFoundException("Specification to delete not found.");
        entity.Specifications.Remove(existing);
    }
}