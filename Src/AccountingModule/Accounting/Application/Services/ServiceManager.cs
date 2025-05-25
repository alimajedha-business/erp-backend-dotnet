using Accounting.Application.Interfaces;
using Accounting.Domain.Interfaces;
using Common.Logging;
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
        
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService  logger)
        {
            _ledgerService = new Lazy<ILedgerService>(() => new
            LedgerService(repositoryManager, logger));            
        }
        public ILedgerService LedgerService => _ledgerService.Value;        
    }
}
