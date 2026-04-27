using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.General.Domain.Entities;
using NGErp.General.Service.Repository.Contracts;

namespace NGErp.General.Infrastructure.DataAccess.Repositories;

public class CompanyRepository(MainDbContext context) :
    Base.Infrastructure.DataAccess.Repositories.Repository<Company>(context),
    ICompanyRepository
{ }
