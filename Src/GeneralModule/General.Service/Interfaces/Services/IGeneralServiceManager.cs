using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Services
{
    public interface IGeneralServiceManager
    {
        ICountryService CountryService{ get; }        
        IDomainService DomainService { get; }
        ICompanyService CompanyService { get; }
    }
}
