using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IPositionJobRepository : IRepositoryWithCompany<PositionJob>
{
    Task<PositionJob?> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );
}
