using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NGErp.Base.Service.Repository.Contract;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Repository.Contracts;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.RequestFeatures;


namespace NGErp.HCM.Service.Repository.Contracts;

public interface IDepartmentRepository : IRepositoryWithCompany<Department>{}
