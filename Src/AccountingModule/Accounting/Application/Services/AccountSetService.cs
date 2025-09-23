using Accounting.Application.DTOs;
using Accounting.Domain.Exceptions;
using Accounting.Application.Interfaces.Repositories;
using Accounting.Application.Interfaces.Services;
using Accounting.Domain.Entities;
using AutoMapper;
using Common.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Services
{
    internal sealed class AccountSetService : IAccountSetService
    {
        private readonly IAccountingRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public AccountSetService(IAccountingRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountSetDto>> GetAllAsync(int ledgerId, bool trackChanges)
        {
            var ledger = await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSets = await _repository.AccountSet.GetAllAsync(ledgerId, trackChanges);
            var accountSetDtos = _mapper.Map<IEnumerable<AccountSetDto>>(accountSets);
            return accountSetDtos;
        }

        public async Task<AccountSetDto?> GetAsync(int ledgerId, int accountSetId, bool trackChanges)
        {
            var ledger =await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSet = await _repository.AccountSet.GetAsync(ledgerId, accountSetId, trackChanges);
            if (accountSet is null)
                throw new AccountSetNotFoundException(accountSetId);
            var accountSetDto = _mapper.Map<AccountSetDto>(accountSet);
            return accountSetDto;
        }

        public async Task<AccountSetDto> CreateAsync(int companyId, int ledgerId, AccountSetForCreationDto accountSet, bool trackChanges)
        {
            var ledger =await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSetEntity = _mapper.Map<AccountSet>(accountSet);
            _repository.AccountSet.Create(companyId, ledgerId, accountSetEntity);
            await _repository.SaveAsync();
            var accountSetDto = _mapper.Map<AccountSetDto>(accountSetEntity);
            return accountSetDto;
        }

        public async Task DeleteAsync(int companyId, int ledgerId, int id, bool trackChanges)
        {
            //var company = _repository.Company.GetCompany(companyId, trackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            var ledger = await _repository.Ledger.GetAsync(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSetForLedger = await _repository.AccountSet.GetAsync(ledgerId, id, trackChanges);
            if (accountSetForLedger is null)
                throw new AccountSetNotFoundException(id);
            _repository.AccountSet.Delete(accountSetForLedger);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int companyId, int ledgerId, int accountSetId, AccountSetForUpdateDto accountSet,
            bool ledTrackChanges, bool accTrackChanges)
        {
            var ledger =await _repository.Ledger.GetAsync(ledgerId, ledTrackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSetEntity = await _repository.AccountSet.GetAsync(ledgerId, accountSetId, accTrackChanges);
            if (accountSetEntity is null)
                throw new AccountSetNotFoundException(accountSetId);
            _mapper.Map(accountSet, accountSetEntity);
            await _repository.SaveAsync();
        }
    }
}
