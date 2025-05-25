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
    internal sealed class LedgerService:ILedgerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerService _logger;

        public LedgerService(IRepositoryManager repository, ILoggerService logger)
        {          
                _repository = repository;
                _logger = logger;        
        }
    }
}
