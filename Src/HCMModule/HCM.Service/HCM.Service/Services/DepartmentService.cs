using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class DepartmentService(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    IValidator<Department> validator,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService
    ) : BaseServiceWithCompany<
        Department,
        DepartmentDto,
        DepartmentParameters,
        IDepartmentRepository,
        HCMResource>
    (
        filterBuilder,
        departmentRepository,
        companyService,
        mapper,
        validator,
        localizer
        ),
    IDepartmentService
{
    protected override string LocalizerKey => "Department";

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        DepartmentChangeStatusDto changeStatusDto,
        CancellationToken ct)
    {
        await EnsureCompanyAsync(companyId, ct);

        var department = await GetByIdOrThrowAsync(companyId, id, ct);
        department.ChangeStatus(changeStatusDto.Status,
            new DateTime((DateOnly)changeStatusDto.Date, TimeOnly.MinValue)
            );
        _repo.Update(department);
        await _repo.SaveChangesAsync(ct);
    }
}