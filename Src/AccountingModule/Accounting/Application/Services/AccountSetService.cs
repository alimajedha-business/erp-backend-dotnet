using Accounting.Application.DTOs;
using Accounting.Application.Exceptions;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using AutoMapper;
using Common.Logging;
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
        private readonly IRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public AccountSetService(IRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<AccountSetDto> GetAllAccountSets(int ledgerId, bool trackChanges)
        {
            var ledger = _repository.Ledger.GetLedger(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSets = _repository.AccountSet.GetAllAccountSets(ledgerId, trackChanges);
            var accountSetDtos = _mapper.Map<IEnumerable<AccountSetDto>>(accountSets);
            return accountSetDtos;
        }

        public AccountSetDto? GetAccountSet(int ledgerId, int accountSetId, bool trackChanges)
        {
            var ledger = _repository.Ledger.GetLedger(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSet = _repository.AccountSet.GetAccountSet(ledgerId, accountSetId, trackChanges);
            if (accountSet is null)
                throw new AccountSetNotFoundException(accountSetId);
            var accountSetDto = _mapper.Map<AccountSetDto>(accountSet);
            return accountSetDto;
        }

        public AccountSetDto CreateAccountSetForLedger(int companyId, int ledgerId, AccountSetForCreationDto accountSet, bool trackChanges)
        {
            var ledger = _repository.Ledger.GetLedger(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSetEntity = _mapper.Map<AccountSet>(accountSet);
            _repository.AccountSet.CreateAccountSetForLedger(companyId, ledgerId, accountSetEntity);
            _repository.Save();
            var accountSetDto = _mapper.Map<AccountSetDto>(accountSetEntity);
            return accountSetDto;
        }

        public void DeleteAccountSetForLedger(int companyId, int ledgerId, int id, bool trackChanges)
        {
            //var company = _repository.Company.GetCompany(companyId, trackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            var ledger = _repository.Ledger.GetLedger(ledgerId, trackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSetForLedger = _repository.AccountSet.GetAccountSet(ledgerId, id, trackChanges);
            if (accountSetForLedger is null)
                throw new AccountSetNotFoundException(id);
            _repository.AccountSet.DeleteAccountSet(accountSetForLedger);
            _repository.Save();
        }

        public void UpdateAccountSetForLedger(int companyId, int ledgerId, int accountSetId, AccountSetForUpdateDto accountSet,
            bool ledTrackChanges, bool accTrackChanges)
        {
            var ledger = _repository.Ledger.GetLedger(ledgerId, ledTrackChanges);
            if (ledger is null)
                throw new LedgerNotFoundException(ledgerId);
            var accountSetEntity = _repository.AccountSet.GetAccountSet(ledgerId, accountSetId,accTrackChanges);
            if (accountSetEntity is null)
                throw new AccountSetNotFoundException(accountSetId);
            _mapper.Map(accountSet, accountSetEntity);
            _repository.Save();
        }
    }
}
