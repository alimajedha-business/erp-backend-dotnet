using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IEmployeeDependantRepository : IRepository<EmployeeDependant>
{
    new Task<EmployeeDependant?> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    IQueryable<EmployeeDependant> GetFiltered(
        Guid employeeId,
        RequestAdvancedFilters requestAdvancedFilters
    );
}
