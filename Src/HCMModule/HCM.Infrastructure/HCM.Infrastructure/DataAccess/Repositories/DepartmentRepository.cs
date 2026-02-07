
using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Infrastructure.DataAccess.Repositories;

public class DepartmentRepository(MainDbContext context) :
    RepositoryWithCompany<Department>(context),
    IDepartmentRepository
{}