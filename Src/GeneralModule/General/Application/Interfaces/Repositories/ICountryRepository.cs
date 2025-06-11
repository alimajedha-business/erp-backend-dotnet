using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAll(bool trackChanges);
        Country? Get(int Id, bool trackChanges);
        void Create(Country country);
        IEnumerable<Country> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void Delete(Country country);
    }
}
