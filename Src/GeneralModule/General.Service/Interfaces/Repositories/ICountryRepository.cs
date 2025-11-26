using NGErp.Base.Service.RequestParameters;
using NGErp.General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        PagedList<Country> GetAll(CountryParameters countryParameters, bool trackChanges);
        Country? Get(int Id, bool trackChanges);
        void Create(Country country);
        IEnumerable<Country> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void Delete(Country country);
    }
}
