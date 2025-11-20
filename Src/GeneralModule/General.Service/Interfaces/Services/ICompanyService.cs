using General.Application.DTOs;
using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto?> GetCompanyForDomainAsync(int domainId, int companyId, bool trackChanges);
        Task<CompanyDto?> GetCompanyAsync(int companyId, bool trackChanges);
    }
}
