using System.Threading;
using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IOrganizationNodeRepository : IRepositoryWithCompany<OrganizationNode>
{
    Task<OrganizationNode?> GetByDepartmentIdAsync(Guid companyid, Guid departemnId, CancellationToken ct);
    Task<OrganizationNode?> GetByPositionIdAsync(Guid companyid, Guid positionId, CancellationToken ct);

}