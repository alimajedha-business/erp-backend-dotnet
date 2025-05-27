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
        AccountSetDto? GetAccountSet(int AccountSetId, bool trackChanges);
    }
}
