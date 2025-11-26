using NGErp.General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Repositories
{
    public interface IProvinceRepository
    {
        IEnumerable<Province> GetAll(int countryId, bool trackChanges);
        Province? Get(int countryId, int provinceId, bool trackChanges);
        void Create(int countryId, Province province);
        IEnumerable<Province> GetByIds(int countryId,IEnumerable<int> provinceIds, bool trackChanges);
        void Delete(Province province);
    }
}
