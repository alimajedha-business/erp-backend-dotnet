using Accounting.Application.Interfaces.Repositories;
using Accounting.Application.Interfaces.Services;
using AutoMapper;
using Common.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILedgerService> _ledgerService;
        private readonly Lazy<IAccountSetService> _accountSetService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper)
        {
            _ledgerService = new Lazy<ILedgerService>(() =>
            new LedgerService(repositoryManager, logger, mapper));
            _accountSetService = new Lazy<IAccountSetService>(() =>
            new AccountSetService(repositoryManager, logger, mapper));
        }
        public ILedgerService LedgerService => _ledgerService.Value;
        public IAccountSetService AccountSetService => _accountSetService.Value;
    }
}
