using AutoMapper;
using AutoMapper.QueryableExtensions;

using Azure.Identity;

using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;
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

public class EmploymentGroupService(
    IEmploymentGroupRepository employmentGroupRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService
    ) : IEmploymentGroupService
{
    private readonly string _key = "EmploymentGroup";
    private readonly IMapper _mapper = mapper;

    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmploymentGroupRepository _repo = employmentGroupRepository;
    private readonly ICompanyService _companyService = companyService;

    public async Task<EmploymentGroupDetailDto> CreateAsync(
        Guid companyId,
        CreateEmploymentGroupDto createDto,
        CancellationToken ct
        )
    {
        // await EnsureCompanyAsync(companyId, ct);
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
        //await EnsureCompanyAsync(companyId, ct);

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
        foreach (var specDto in updateDto.Specifications.Where(s => s.OperationType == OperationType.Delete))
        {
            HandleDeleteSpecification(entity, specDto);
        }

        foreach (var specDto in updateDto.Specifications.Where(s => s.OperationType == OperationType.Create))
        {
            HandleCreateSpecification(entity, specDto);
        }

        // 5. Save changes once

        await _repo.SaveChangesAsync(ct);

        // 6. Map and return updated result

        return new EmploymentGroupDetailDto
        {
            Id = entity.Id,
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
        // await EnsureCompanyAsync(companyId, ct);
        var entity = await _repo.GetWithSpecificationsAsync(companyId, id, ct);

        return entity is null
            ? null
            : _mapper.Map<EmploymentGroupDetailDto>(entity);
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
        var existing = entity.Specifications.FirstOrDefault(
            x => x.Id == dto.Id && x.EmploymentGroupId == entity.Id);
        if (existing == null)
            throw new NotFoundException("Specification to delete not found.");
        entity.Specifications.Remove(existing);
    }

    //async Task<EmploymentGroupDetailDto?> IEmploymentGroupService.GetByIdAsync(
    //     Guid companyId,
    //     Guid id,
    //     CancellationToken ct
    //     )
    //{
    //    await EnsureCompanyAsync(companyId, ct);
    //    var entity = await _employmentGroupRepository
    //   .Find(companyId, e => e.Id == id)
    //   .AsNoTracking()
    //   .ProjectTo<EmploymentGroupDetailDto>(_mapper.ConfigurationProvider)
    //   .FirstOrDefaultAsync(ct);

    //    return entity ?? throw new NotFoundException(_localizer[_key].Value);
    //}

    public async Task<ListResponseModel<EmploymentGroupDto>> GetFilteredAsync(
      Guid companyId,
      EmploymentGroupParameters parameters,
      FilterNodeDto? filterNodeDto = null,
      CancellationToken ct = default
  )
    {
        var advancedFilters = _filterBuilder.Build<EmploymentGroupDto>(filterNodeDto);
        var query = _repo.GetFiltered(companyId, advancedFilters);
        var res = await _repo.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmploymentGroupDto>(
            results: _mapper.Map<IReadOnlyList<EmploymentGroupDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task DeleteAsync(
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

        _repo.Remove(entity);
        await _repo.SaveChangesAsync(ct);
    }

    private async Task<EmploymentGroup> GetByIdOrThrowAsync(
    Guid companyId,
    Guid id,
    bool trackChanges = false,
    CancellationToken ct = default
)
    {
        // TODO: add specification if needed
        var entity = await _repo.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}