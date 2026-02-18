using AutoMapper;

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

public class DepartmentService(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
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
        localizer
        ),
    IDepartmentService
{
    protected override string LocalizerKey => "Department";
    private readonly IDepartmentRepository _departmentRepository = departmentRepository;

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool newStatus,
        CancellationToken ct)
    {
        await EnsureCompanyAsync(companyId, ct);
        var department = await GetByIdOrThrowAsync(companyId, id, ct);

        department.ChangeStatus(newStatus, DateTime.UtcNow);

        _departmentRepository.Update(department);
        await _departmentRepository.SaveChangesAsync(ct);
    }

    public Task<DepartmentDto> CreateDepartmentAsync(
        Guid companyId,
        CreateDepartmentDto createDto,
        CancellationToken ct
        ) => CreateAsync(companyId, createDto, ct);

    public Task DeleteDepartmentAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        ) => DeleteAsync(companyId, id, ct);

    public Task<ListResponseModel<DepartmentDto>> GetAllDepartmentsAsync(
        Guid companyId,
        DepartmentParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
        ) => GetAllAsync(companyId, parameters, ct, filterNodeDto);

    public Task<DepartmentDto> GetDepartmentByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        ) => GetByIdAsync(companyId, id, ct);

    public Task<DepartmentDto> PatchDepartmentAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchDepartmentDto> patchDocument,
        CancellationToken ct
        ) => PatchAsync(companyId, id, patchDocument, ct);
}