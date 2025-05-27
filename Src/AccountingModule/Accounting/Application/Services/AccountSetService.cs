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

        public AccountSetDto? GetAccountSet(int AccountSetId, bool trackChanges)
        {
            var accountSet = _repository.AccountSet.GetAccountSet(AccountSetId, trackChanges);
            if (accountSet is null)
                throw new AccountSetNotFoundException(AccountSetId);
            var accountSetDto = _mapper.Map<AccountSetDto>(accountSet);
            return accountSetDto;
        }
    }
}
