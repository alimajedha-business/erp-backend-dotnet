using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IEmployeeWorkExperienceRepository : IRepository<EmployeeWorkExperience>
{
    Task<EmployeeWorkExperience?> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    IQueryable<EmployeeWorkExperience> GetFiltered(
        Guid employeeId,
        RequestAdvancedFilters requestAdvancedFilters
    );
}
