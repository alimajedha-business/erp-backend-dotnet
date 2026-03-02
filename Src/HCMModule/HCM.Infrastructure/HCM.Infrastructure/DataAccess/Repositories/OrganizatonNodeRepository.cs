using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class OrganizationNodeRepository(MainDbContext context) :
    RepositoryWithCompany<OrganizationNode>(context),
    IOrganizationNodeRepository
{ }