using Common.Infrastructure.DataAccess;
using General.Application.Interfaces.Repositories;
using General.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(GeneralDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public Country? Get(int Id, bool trackChanges) =>
            FindByCondition(x => x.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Country> GetAll(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Id)
            .ToList();

        public IEnumerable<Country> GetByIds(IEnumerable<int> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();
        public new void Create(Country country)
        {
            base.Create(country);
        }

        public new void Delete(Country country) => base.Delete(country);
    }
}
