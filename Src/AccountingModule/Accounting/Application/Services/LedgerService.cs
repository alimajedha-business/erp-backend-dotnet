using Accounting.Application.DTOs;
using Accounting.Application.Exceptions;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using AutoMapper;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Services
{
    internal sealed class LedgerService : ILedgerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public LedgerService(IRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<LedgerDto> GetAllLedgers(bool trackChanges)
        {
            var ledgers = _repository.Ledger.GetAllLedgers(trackChanges);
            var ledgersDto = _mapper.Map<IEnumerable<LedgerDto>>(ledgers);
            return ledgersDto;
        }

        public LedgerDto? GetLedger(int LedgerId, bool trackChanges)
        {
            var ledger = _repository.Ledger.GetLedger(LedgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(LedgerId);
            var ledgerDto = _mapper.Map<LedgerDto>(ledger);
            return ledgerDto;
        }
    }
}
