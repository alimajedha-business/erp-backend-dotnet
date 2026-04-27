using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;
public interface IEmployeeRepository: IRepositoryWithCompany<Employee>
{ 
    Task<Employee?> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );


}