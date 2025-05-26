using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Services
{
    internal sealed class LedgerService : ILedgerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerService _logger;

        public LedgerService(IRepositoryManager repository, ILoggerService logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<LedgerDto> GetAllLedgers(bool trackChanges)
        {
            try
            {
                var ledgers = _repository.Ledger.GetAllLedgers(trackChanges);
                var ledgersDto = ledgers.Select(l => new LedgerDto(l.Code, l.Name ?? "", l.Description ?? "")).ToList();
                return ledgersDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLedgers)}  service method {ex}");
                throw;
            }
        }
    }
}
