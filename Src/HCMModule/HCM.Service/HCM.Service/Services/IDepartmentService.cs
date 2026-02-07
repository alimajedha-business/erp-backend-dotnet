using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartmentsAsync(
            Guid companyId,
            DepartmentParameters departmentParameters,
            RequestAdvancedFilters? requestAdvancedFilters = null
            );
        Task<DepartmentDto?> GetDepartmentByIdAsync(
            Guid companyId, 
            Guid id, 
            CancellationToken ct
            );
        Task<DepartmentDto> CreateDepartmentAsync(
            Guid companyId, 
            CreateDepartmentDto createDepartmentDto, 
            CancellationToken ct
            );
        Task<DepartmentDto> UpdateDepartmentAsync(
            Guid companyId, 
            Guid id, 
            UpdateDepartmentDto dto
            );
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
