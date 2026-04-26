using AutoMapper;

using FluentValidation;

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
    private static readonly DateOnly EmptyValidFromFallback = new(1921, 3, 21);
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
        await _companyService.GetByIdAsync(companyId, ct);

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
            ValidFrom = NormalizeEmptyValidFrom(specDto.ValidFrom)
        };

        employmentGroup.Specifications.Add(specification);

        await _repo.AddAsync(employmentGroup, ct);
        await _repo.SaveChangesAsync(ct);

        return new EmploymentGroupDetailDto
        {
            Id = employmentGroup.Id,
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
        await _companyService.GetByIdAsync(companyId, ct);

        // 1. Load group with all specifications (aggregate root)

        var entity = await _repo.GetWithSpecificationsAsync(companyId, id, ct);

        if (entity == null)
            throw new NotFoundException(_localizer[_key].Value);

        // 2. Check name uniqueness
        var nameExists = await _repo.AnyAsync(e => e.CompanyId == companyId
        && e.Name == updateDto.Name
        && e.Id != id
        );

        if (nameExists)
            throw new InvalidOperationException("نام گروه استخدامی در شرکت باید یکتا باشد");

        // 3. Update root values
        entity.Name = updateDto.Name;

        // 4. Process specifications based on OperationEnum
        foreach (var specDto in updateDto.Specifications.Where(
            s => s.OperationType == SpecificationOperationType.Delete))
        {
            HandleDeleteSpecification(entity, specDto);
        }

        foreach (var specDto in updateDto.Specifications.Where(
            s => s.OperationType == SpecificationOperationType.Create))
        {
            HandleCreateSpecification(entity, specDto);
        }

        NormalizeSpecificationRanges(entity);

        // 5. Save changes once

        await _repo.SaveChangesAsync(ct);

        // 6. Map and return updated result

        return new EmploymentGroupDetailDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Specifications = entity.Specifications
            .OrderBy(s => s.ValidFrom)
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
        await _companyService.GetByIdAsync(companyId, ct);

        var entity = await _repo.GetWithSpecificationsAsync(companyId, id, ct);

        return entity is null
            ? null
            : _mapper.Map<EmploymentGroupDetailDto>(entity);
    }

    private void HandleCreateSpecification(
        EmploymentGroup entity,
        UpdateEmploymentGroupSpecificationDto dto)
    {
        if (entity.Specifications.Any(x => x.ValidFrom == dto.ValidFrom))
            throw new InvalidOperationException("برای هر تاریخ شروع فقط یک مشخصات گروه استخدامی مجاز است");

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
        if (!dto.Id.HasValue)
            throw new InvalidOperationException("Specification id is required for delete operation.");

        var existing = entity.Specifications.FirstOrDefault(
            x => x.Id == dto.Id.Value && x.EmploymentGroupId == entity.Id);
        if (existing == null)
            throw new NotFoundException("Specification to delete not found.");

        _repo.RemoveSpecification(existing);
        entity.Specifications.Remove(existing);
    }

    private static DateOnly NormalizeEmptyValidFrom(DateOnly validFrom)
    {
        // ASP.NET binds an empty DateOnly input to its default value.
        return validFrom == DateOnly.MinValue ? EmptyValidFromFallback : validFrom;
    }

    private static void NormalizeSpecificationRanges(EmploymentGroup entity)
    {
        var orderedSpecifications = entity.Specifications
            .OrderBy(x => x.ValidFrom)
            .ToList();

        if (orderedSpecifications.Count == 0)
            throw new InvalidOperationException("حداقل یک مشخصات برای گروه استخدامی الزامی است");

        for (var i = 0; i < orderedSpecifications.Count - 1; i++)
        {
            var current = orderedSpecifications[i];
            var next = orderedSpecifications[i + 1];

            if (current.ValidFrom >= next.ValidFrom)
                throw new InvalidOperationException("تاریخ شروع مشخصات گروه استخدامی باید یکتا و صعودی باشد");

            current.ValidTo = next.ValidFrom.AddDays(-1);
        }

        orderedSpecifications[^1].ValidTo = null;
    }

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
        var employmentGroup = await _repo
         .GetWithSpecificationsAsync(companyId, id, ct);

        if (employmentGroup is null)
            throw new NotFoundException(_localizer[_key].Value);

        // TODO: Check if EmploymentGroup is used

        await _repo.DeleteAsync(employmentGroup, ct);
    }
}
