using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Service.Interfaces.Repositories;
using NGErp.General.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Infrastructure.DataAccess.Repositories
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(MainDbContext repositoryContext) : base(repositoryContext)
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
