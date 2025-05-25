using Accounting.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly AccountingDbContext _repositoryContext;
        private readonly Lazy<ILedgerRepository> _ledgerRepository;

        public RepositoryManager(AccountingDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _ledgerRepository = new Lazy<ILedgerRepository>(() => new
            LedgerRepository(repositoryContext));
        }
        public ILedgerRepository Ledger => _ledgerRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();

    }
}