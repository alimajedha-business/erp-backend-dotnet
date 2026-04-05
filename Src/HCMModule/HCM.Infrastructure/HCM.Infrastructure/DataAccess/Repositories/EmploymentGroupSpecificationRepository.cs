using NGErp.Base.Infrastructure.DataAccess;

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.Base.Infrastructure.DataAccess.Repositories;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class EmploymentGroupSpecificationRepository(MainDbContext context) :
    Repository<EmploymentGroupSpecification>(context),
    IEmploymentGroupSpecificationRepository
{ }