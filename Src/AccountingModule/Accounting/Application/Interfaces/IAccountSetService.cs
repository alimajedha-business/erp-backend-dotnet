using Accounting.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Interfaces
{
    public interface IAccountSetService
    {
        IEnumerable<AccountSetDto> GetAllAccountSets(int ledgerId, bool trackChanges);
        AccountSetDto? GetAccountSet(int ledgerId, int accountSetId, bool trackChanges);
        AccountSetDto CreateAccountSetForLedger(int companyId, int ledgerId, AccountSetForCreationDto accountSet,
            bool trackChanges);
        void DeleteAccountSetForLedger(int companyId, int ledgerId, int id, bool trackChanges);
        void UpdateAccountSetForLedger(int companyId, int ledgerId,int accountSetId,AccountSetForUpdateDto accountSet, bool ledTrackChanges, 
            bool accTrackChanges);
    }
}
