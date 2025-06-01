using Accounting.Application.DTOs;
using Accounting.Application.Exceptions;
using Accounting.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Common.Domain.Exceptions;
using Accounting.Application.Interfaces.Services;
using Accounting.Application.Interfaces.Repositories;
using Common.Infrastructure.Logging;


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

        public LedgerDto Create(LedgerForCreationDto ledger)
        {
            var ledgerEntity = _mapper.Map<Ledger>(ledger);  
            _repository.Ledger.Create(ledgerEntity);
            _repository.Save();
            var ledgerToReturn = _mapper.Map<LedgerDto>(ledgerEntity);
            return ledgerToReturn;
        }

        public (IEnumerable<LedgerDto> ledgers, string ids) CreateCollection(IEnumerable<LedgerForCreationDto> ledgerCollection)
        {
            if (ledgerCollection is null)
                throw new LedgerCollectionBadRequestException();
            var ledgerEntities = _mapper.Map<IEnumerable<Ledger>>(ledgerCollection);
            foreach (var ledger in ledgerEntities)
            {
                _repository.Ledger.Create(ledger);
            }
            _repository.Save();
            var ledgerCollectionToReturn =
            _mapper.Map<IEnumerable<LedgerDto>>(ledgerEntities);
            var ids = string.Join(",", ledgerCollectionToReturn.Select(c => c.Id));
            return (ledgers: ledgerCollectionToReturn, ids);
        }

        public void Delete(int ledgerId, bool trackChanges)
        {
            var ledger = _repository.Ledger.Get(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            _repository.Ledger.Delete(ledger);
            _repository.Save();
        }

        public IEnumerable<LedgerDto> GetAll(bool trackChanges)
        {
            var ledgers = _repository.Ledger.GetAll(trackChanges);
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

        public LedgerDto? Get(int LedgerId, bool trackChanges)
        {
            var ledger = _repository.Ledger.Get(LedgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(LedgerId);
            var ledgerDto = _mapper.Map<LedgerDto>(ledger);
            return ledgerDto;
        }

        public void Update(int ledgerId, LedgerForUpdateDto ledger, bool trackChanges)
        {
            var ledgerEntity = _repository.Ledger.Get(ledgerId, trackChanges);
            if (ledgerEntity is null)
                throw new LedgerNotFoundException(ledgerId);
            _mapper.Map(ledger, ledgerEntity);
            _repository.Save();
        }

        public (LedgerForUpdateDto ledgerForUpdate, Ledger ledgerEntity) GetLedgerForPatch(int ledgerId, bool trackChanges)
        {
            var ledgerEntity = _repository.Ledger.Get(ledgerId, trackChanges);
            if (ledgerEntity is null)
                throw new LedgerNotFoundException(ledgerId);
            var ledgerToPatch = _mapper.Map<LedgerForUpdateDto>(ledgerEntity);
            return (ledgerToPatch, ledgerEntity);
        }

        public void SaveChangesForPatch(LedgerForUpdateDto ledgerToPatch, Ledger ledgerEntity)
        {
            _mapper.Map(ledgerToPatch, ledgerEntity);
            _repository.Save();
        }
    }
}
