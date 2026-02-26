using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationNodeService : IBaseServiceWithCompany<
    OrganizationNode,

    >
{
}