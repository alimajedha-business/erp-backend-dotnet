using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationalStructureService
{
    Task<List<OrganizationalStructureDto>> GetTreeAsync(
        Guid companyId, 
        DateOnly date
        );
    //Task<Guid> AddNodeAsync(AddOrgNodeDto dto);
    //Task MoveNodeAsync(Guid nodeId, Guid newParentId, DateOnly effectiveDate);
    //Task DeactivateNodeAsync(Guid nodeId, DateOnly date);
    //Task<List<OrgNodeHistoryDto>> GetHistoryAsync(Guid nodeBusinessId);
    //Task<List<EmployeeDto>> GetEmployeesOfPositionAsync(Guid nodeId);
}
