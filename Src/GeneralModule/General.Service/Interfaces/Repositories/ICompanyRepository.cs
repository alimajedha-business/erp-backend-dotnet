using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Service.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company?> GetCompanyForDomainAsync(int domainId, int companyId, bool trackChanges);
        Task<Company?> GetCompanyAsync(int companyId, bool trackChanges);
    }
}
