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
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }

        public Currency? Get(int Id, bool trackChanges) =>
            FindByCondition(x => x.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Currency> GetAll(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Id)
            .ToList();

        public IEnumerable<Currency> GetByIds(IEnumerable<int> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public new void Create(Currency currency)
        {
            base.Create(currency);
        }

        public new void Delete(Currency currency) => base.Delete(currency);
    }
}
