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
      Task<IEnumerable<AccountSetDto>> GetAllAsync(int ledgerId, bool trackChanges);
        Task<AccountSetDto?> GetAsync(int ledgerId, int accountSetId, bool trackChanges);
        Task<AccountSetDto> CreateAsync(int companyId, int ledgerId, AccountSetForCreationDto accountSet, bool trackChanges);
        Task DeleteAsync(int companyId, int ledgerId, int id, bool trackChanges);
        Task UpdateAsync(int companyId, int ledgerId, int accountSetId, AccountSetForUpdateDto accountSet, bool ledTrackChanges,
            bool accTrackChanges);
    }
}
