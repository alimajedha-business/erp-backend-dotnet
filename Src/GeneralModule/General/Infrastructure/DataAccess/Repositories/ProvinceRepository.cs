using Common.Application.Interfaces;
using Common.Infrastructure.DataAccess;
using General.Application.Interfaces.Repositories;
using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories
{
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(IMainDbContext context) : base(context)
        {
        }

        public IEnumerable<Province> GetAll(int countryId, bool trackChanges) =>
            FindByCondition(p => p.CountryId.Equals(countryId), trackChanges)
            .OrderBy(p => p.Name)
            .ToList();

        public Province? Get(int countryId, int provinceId, bool trackChanges) =>
            FindByCondition(p => p.CountryId.Equals(countryId) && p.CountryId.Equals(provinceId), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Province> GetByIds(int countryId,IEnumerable<int> provinceIds, bool trackChanges) =>
            FindByCondition(p => p.CountryId.Equals(countryId) && provinceIds.Contains(p.Id), trackChanges)
            .ToList();

        public void Create(int countryId, Province province)
        {
            province.CountryId = countryId;
            base.Create(province);
        }

        public new void Delete(Province province) => base.Delete(province);
    }
}
