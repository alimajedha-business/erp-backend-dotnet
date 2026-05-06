using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class PositionJobRepository(MainDbContext context) :
    Repository<PositionJob>(context),
    IPositionJobRepository
{ }
