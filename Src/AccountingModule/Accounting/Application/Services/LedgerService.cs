using Accounting.Application.DTOs;
using Accounting.Domain.Exceptions;
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
        private readonly IAccountingRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public LedgerService(IAccountingRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LedgerDto> CreateAsync(LedgerForCreationDto ledger)
        {
            var ledgerEntity = _mapper.Map<Ledger>(ledger);
            _repository.Ledger.Create(ledgerEntity);
            await _repository.SaveAsync();
            var ledgerToReturn = _mapper.Map<LedgerDto>(ledgerEntity);
            return ledgerToReturn;
        }

        public async Task<(IEnumerable<LedgerDto> ledgers, string ids)> CreateCollectionAsync(IEnumerable<LedgerForCreationDto> ledgerCollection)
        {
            if (ledgerCollection is null)
                throw new LedgerCollectionBadRequestException();
            var ledgerEntities = _mapper.Map<IEnumerable<Ledger>>(ledgerCollection);
            foreach (var ledger in ledgerEntities)
            {
                _repository.Ledger.Create(ledger);
            }
            await _repository.SaveAsync();
            var ledgerCollectionToReturn =
            _mapper.Map<IEnumerable<LedgerDto>>(ledgerEntities);
            var ids = string.Join(",", ledgerCollectionToReturn.Select(c => c.Id));
            return (ledgers: ledgerCollectionToReturn, ids);
        }

        public async Task DeleteAsync(int ledgerId, bool trackChanges)
        {
            var ledger = await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            _repository.Ledger.Delete(ledger);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<LedgerDto>> GetAllAsync(bool trackChanges)
        {
            var ledgers = await _repository.Ledger.GetAllAsync(trackChanges);
            var ledgersDto = _mapper.Map<IEnumerable<LedgerDto>>(ledgers);
            return ledgersDto;
        }

        public async Task<IEnumerable<LedgerDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var ledgerEntities = await _repository.Ledger.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != ledgerEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var ledgersToReturn = _mapper.Map<IEnumerable<LedgerDto>>(ledgerEntities);
            return ledgersToReturn;
        }

        public async Task<LedgerDto?> GetAsync(int LedgerId, bool trackChanges)
        {
            var ledger = await _repository.Ledger.GetAsync(LedgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(LedgerId);
            var ledgerDto = _mapper.Map<LedgerDto>(ledger);
            return ledgerDto;
        }

        public async Task UpdateAsync(int ledgerId, LedgerForUpdateDto ledger, bool trackChanges)
        {
            var ledgerEntity = await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledgerEntity is null)
                throw new LedgerNotFoundException(ledgerId);
            _mapper.Map(ledger, ledgerEntity);
            await _repository.SaveAsync();
        }

        public async Task<(LedgerForUpdateDto ledgerForUpdate, Ledger ledgerEntity)> GetLedgerForPatchAsync(int ledgerId, bool trackChanges)
        {
            var ledgerEntity = await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledgerEntity is null)
                throw new LedgerNotFoundException(ledgerId);
            var ledgerToPatch = _mapper.Map<LedgerForUpdateDto>(ledgerEntity);
            return (ledgerToPatch, ledgerEntity);
        }

        public async Task SaveChangesForPatchAsync(LedgerForUpdateDto ledgerToPatch, Ledger ledgerEntity)
        {
            _mapper.Map(ledgerToPatch, ledgerEntity);
            await _repository.SaveAsync();
        }
    }
}
