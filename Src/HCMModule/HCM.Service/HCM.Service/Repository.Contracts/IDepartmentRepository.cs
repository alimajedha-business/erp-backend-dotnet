using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IDepartmentRepository : IRepositoryWithCompany<Department> { }
{    
    Task<ListQueryResult<Department>> GetAllAsync(
        Guid companyId,
        DepartmentParameters departmentParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );

}
