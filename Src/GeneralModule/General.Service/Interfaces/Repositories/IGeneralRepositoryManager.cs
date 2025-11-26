using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Repositories
{
    public interface IGeneralRepositoryManager
    {
        ICountryRepository Country { get; }
        ICurrencyRepository Currency { get; }
        IProvinceRepository Province { get; }
        IDomainRepository Domain { get; }
        ICompanyRepository Company { get; }

        void Save();
    }
}
