using NGErp.General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company?> GetCompanyForDomainAsync(Guid domainId, Guid companyId);
        Task<Company?> GetCompanyAsync(Guid companyId);
    }
}
