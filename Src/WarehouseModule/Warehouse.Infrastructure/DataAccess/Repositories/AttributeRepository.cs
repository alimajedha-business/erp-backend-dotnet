using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class AttributeRepository(MainDbContext context) :
    RepositoryWithCompany<Domain.Entities.Attribute>(context),
    IAttributeRepository
{ }
