using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class WarehouseRepository(MainDbContext context) :
    RepositoryWithCompany<Domain.Entities.Warehouse>(context),
    IWarehouseRepository
{ }