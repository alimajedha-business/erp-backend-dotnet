using Common.Application.RequestParameters;
using Common.Infrastructure.DataAccess;
using General.Application.Interfaces.Repositories;
using General.Domain.Entities;
using General.Infrastructure.DataAccess.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

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

        public PagedList<Country> GetAll(CountryParameters countryParameters, bool trackChanges)
        {
            var countries = FindAll(trackChanges)
            .OrderBy(x => x.Id)
            .Search(countryParameters.SearchTerm)
            .Skip((countryParameters.PageNumber - 1) * countryParameters.PageSize)
            .Take(countryParameters.PageSize)
            .Include(c => c.Currency)
            .Sort(countryParameters.OrderBy)
            .ToList();

            var count = FindAll(trackChanges).Count();
            return PagedList<Country>
                .ToPagedList(countries, count, countryParameters.PageNumber, countryParameters.PageSize);
        }

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
