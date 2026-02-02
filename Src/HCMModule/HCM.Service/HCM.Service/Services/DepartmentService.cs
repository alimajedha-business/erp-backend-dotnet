
using AutoMapper;

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
    public Task<DepartmentDto> CreateDepartmentAsync(Guid companyId, CreateDepartmentDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<DepartmentDto> CreateDepartmentAsync(Guid companyId, CreateDepartmentDto createDepartmentDto, CancellationToken ct)
    {
        var company = _integrationService.GetCompanyByIdAsync(companyId);
        if (company == null)
            throw new NotFoundException(_generalLocalizer["Company"].Value);

        var department = _mapper.Map<Department>(createDepartmentDto);
        department.CompanyId = companyId;

        var createdDepartment = await _departmentRepository.AddAsync(department, ct);
        await _departmentRepository.SaveChangesAsync(ct);

        return _mapper.Map<DepartmentDto>(createdDepartment);
    }

    public Task<bool> DeleteDepartmentAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DepartmentDto>> GetAllDepartmentsAsync(
        Guid companyId, DepartmentParameters departmentParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null)
    {
        //var listQueryResult = await _departmentRepository.GetAllAsync(
        //     companyId,
        //     departmentParameters,
        //     requestAdvancedFilters
        // );

        //return new ListResponseModel<DepartmentDto>(
        //    items: _mapper.Map<IReadOnlyList<DepartmentDto>>(listQueryResult.items),
        //    totalCount: listQueryResult.count, departmentParameters
        //);
        throw new NotImplementedException();
    }

    public Task<DepartmentDto?> GetDepartmentByIdAsync(Guid companyId, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<DepartmentDto> UpdateDepartmentAsync(Guid companyId, Guid id, UpdateDepartmentDto dto)
    {
        throw new NotImplementedException();
    }
}
