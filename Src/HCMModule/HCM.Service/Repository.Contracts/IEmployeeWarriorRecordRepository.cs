using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IEmployeeWarriorRecordRepository : IRepository<EmployeeWarriorRecord>
{ 
    Task<EmployeeWarriorRecord?> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    IQueryable<EmployeeWarriorRecord> GetFiltered(
        Guid employeeId,
        RequestAdvancedFilters requestAdvancedFilters
    );

}
