using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IEmployeeRelativeRepository : IRepository<EmployeeRelative>
{
    Task<EmployeeRelative?> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    IQueryable<EmployeeRelative> GetFiltered(
        Guid employeeId,
        RequestAdvancedFilters requestAdvancedFilters
    );
}
