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
using Common.Exceptions;

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

        public LedgerDto CreateLedger(LedgerForCreationDto ledger)
        {
            var ledgerEntity = _mapper.Map<Ledger>(ledger);
            _repository.Ledger.CreateLedger(ledgerEntity);
            _repository.Save();
            var ledgerToReturn = _mapper.Map<LedgerDto>(ledgerEntity);
            return ledgerToReturn;
        }

        public (IEnumerable<LedgerDto> ledgers, string ids) CreateLedgerCollection(IEnumerable<LedgerForCreationDto> ledgerCollection)
        {
            if (ledgerCollection is null)
                throw new LedgerCollectionBadRequestException();
            var ledgerEntities = _mapper.Map<IEnumerable<Ledger>>(ledgerCollection);
            foreach (var ledger in ledgerEntities)
            {
                _repository.Ledger.CreateLedger(ledger);
            }
            _repository.Save();
            var ledgerCollectionToReturn =
            _mapper.Map<IEnumerable<LedgerDto>>(ledgerEntities);
            var ids = string.Join(",", ledgerCollectionToReturn.Select(c => c.Id));
            return (ledgers: ledgerCollectionToReturn, ids);
        }

        public IEnumerable<LedgerDto> GetAllLedgers(bool trackChanges)
        {
            var ledgers = _repository.Ledger.GetAllLedgers(trackChanges);
            var ledgersDto = _mapper.Map<IEnumerable<LedgerDto>>(ledgers);
            return ledgersDto;
        }

        public IEnumerable<LedgerDto> GetByIds(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var ledgerEntities = _repository.Ledger.GetByIds(ids, trackChanges);
            if (ids.Count() != ledgerEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var ledgersToReturn = _mapper.Map<IEnumerable<LedgerDto>>(ledgerEntities);
            return ledgersToReturn;
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
