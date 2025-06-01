using Accounting.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Interfaces.Services
{
    public interface IAccountSetService
    {
        IEnumerable<AccountSetDto> GetAll(int ledgerId, bool trackChanges);
        AccountSetDto? Get(int ledgerId, int accountSetId, bool trackChanges);
        AccountSetDto Create(int companyId, int ledgerId, AccountSetForCreationDto accountSet,
            bool trackChanges);
        void Delete(int companyId, int ledgerId, int id, bool trackChanges);
        void Update(int companyId, int ledgerId, int accountSetId, AccountSetForUpdateDto accountSet, bool ledTrackChanges,
            bool accTrackChanges);
    }
}
