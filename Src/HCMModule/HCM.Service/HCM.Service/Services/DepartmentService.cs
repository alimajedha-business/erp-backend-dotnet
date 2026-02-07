using AutoMapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Resources;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NGErp.HCM.Service.Services;

public class DepartmentService(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IPythonIntegrationService integrationService,
    IStringLocalizer<GeneralResource> generalLocalizer
    ) : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;
    private readonly IStringLocalizer<GeneralResource> _generalLocalizer = generalLocalizer;
    private readonly IPythonIntegrationService _integrationService = integrationService;

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool newStatus,
        CancellationToken ct)
    {
        var department = await GetByIdOrThrowExceptionAsync(companyId, id, ct);

        department.ChangeStatus(newStatus, DateTime.UtcNow);

        _departmentRepository.Update(department);
        await _departmentRepository.SaveChangesAsync(ct);
    }

    public async Task<DepartmentDto> CreateDepartmentAsync(
        Guid companyId,
        CreateDepartmentDto createDepartmentDto,
        CancellationToken ct
        )
    {
        var department = _mapper.Map<Department>(createDepartmentDto);
        department.CompanyId = companyId;

        var createdDepartment = await _departmentRepository.AddAsync(department, ct);
        await _departmentRepository.SaveChangesAsync(ct);

        return _mapper.Map<DepartmentDto>(createdDepartment);
    }

    public async Task<bool> DeleteDepartmentAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct)
    {
        var department = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _departmentRepository.Remove(department);

        try
        {
            await _departmentRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["Category"].Value);
        }

        return true;
    }

    public async Task<ListResponseModel<DepartmentDto>> GetAllDepartmentsAsync(
        Guid companyId,
        DepartmentParameters departmentParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
        )
    {
        var listQueryResult = await _departmentRepository.GetAllAsync(
          companyId,
          departmentParameters,
          ct,
          requestAdvancedFilters
          );

        return new ListResponseModel<DepartmentDto>(
            items: _mapper.Map<IReadOnlyList<DepartmentDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            departmentParameters
        );
    }

    public async Task<DepartmentDto?> GetDepartmentByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
        )
    {
        var department = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        return _mapper.Map<DepartmentDto>(department);
    }

    public Task<DepartmentDto> UpdateDepartmentAsync(
        Guid companyId,
        Guid id,
        UpdateDepartmentDto dto
        )
    {
        throw new NotImplementedException();
    }

    private async Task<Department> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
        )
    {
        var department = await _departmentRepository.GetByIdAsync(companyId, id, ct, trackChanges);
        return department ?? throw new NotFoundException(_localizer["Department"].Value);
    }
}
