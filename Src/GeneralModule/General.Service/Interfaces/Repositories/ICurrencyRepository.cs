using NGErp.General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetAll(bool trackChanges);
        Currency? Get(int Id, bool trackChanges);
        void Create(Currency currency);
        IEnumerable<Currency> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void Delete(Currency currency);
    }
}
