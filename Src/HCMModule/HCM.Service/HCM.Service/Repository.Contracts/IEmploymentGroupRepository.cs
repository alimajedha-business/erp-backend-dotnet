using System.Linq.Expressions;

using AutoMapper;

using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Repository.Contracts;

public interface IEmploymentGroupRepository : IRepositoryWithCompany<EmploymentGroup>
{
    Task<EmploymentGroup?> GetWithSpecificationsAsync(Guid companyId, Guid id, CancellationToken ct);
}