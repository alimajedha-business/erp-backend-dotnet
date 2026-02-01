using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;


namespace NGErp.HCM.Service.Services;

public class DepartmentService(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer) : IDepartmentService
{

    private readonly IDepartmentRepository _departmentRepository = departmentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<HCMResource> _localizer = localizer;
    public Task<DepartmentDto> CreateDepartmentAsync(Guid companyId, CreateDepartmentDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDepartmentAsync(int id)
    {
        throw new NotImplementedException();
    }

    public  Task<List<DepartmentDto>> GetAllDepartmentsAsync(
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
