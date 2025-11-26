using NGErp.General.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto?> GetCompanyForDomainAsync(int domainId, int companyId, bool trackChanges);
        Task<CompanyDto?> GetCompanyAsync(int companyId, bool trackChanges);
    }
}
