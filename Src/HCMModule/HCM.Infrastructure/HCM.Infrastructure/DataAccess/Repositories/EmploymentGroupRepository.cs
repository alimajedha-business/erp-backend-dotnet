using NGErp.Base.Infrastructure.DataAccess;

using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.Base.Infrastructure.DataAccess.Repositories;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmploymentGroupRepository(MainDbContext context) :
    RepositoryWithCompany<EmploymentGroup>(context),
    IEmploymentGroupRepository
{ }