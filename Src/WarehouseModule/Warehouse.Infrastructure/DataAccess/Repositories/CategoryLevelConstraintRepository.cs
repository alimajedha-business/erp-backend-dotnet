using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryLevelConstraintRepository(MainDbContext context) :
    RepositoryWithCompany<CategoryLevelConstraint>(context),
    ICategoryLevelConstraintRepository
{ }
