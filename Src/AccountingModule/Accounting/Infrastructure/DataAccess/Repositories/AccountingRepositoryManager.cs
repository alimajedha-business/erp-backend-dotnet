using Accounting.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.DataAccess.Repositories
{
    public sealed class AccountingRepositoryManager : IRepositoryManager
    {
        private readonly AccountingDbContext _repositoryContext;
        private readonly Lazy<ILedgerRepository> _ledgerRepository;
        private readonly Lazy<IAccountSetRepository> _accountSetRepository;
        public AccountingRepositoryManager(AccountingDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _ledgerRepository = new Lazy<ILedgerRepository>(() => 
            new LedgerRepository(repositoryContext));
            _accountSetRepository = new Lazy<IAccountSetRepository>(() => 
            new AccountSetRepository(repositoryContext));
        }
        public ILedgerRepository Ledger => _ledgerRepository.Value;
        public IAccountSetRepository AccountSet => _accountSetRepository.Value;
        public void Save() => _repositoryContext.SaveChanges();

    }
}