using Accounting.Application.DTOs;
using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Interfaces.Services
{
    public interface ILedgerService
    {
        Task<IEnumerable<LedgerDto>> GetAllAsync(bool trackChanges);
        Task<LedgerDto?> GetAsync(int LedgerId, bool trackChanges);
        Task<LedgerDto> CreateAsync(LedgerForCreationDto ledger);
        Task<IEnumerable<LedgerDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
       Task<(IEnumerable<LedgerDto> ledgers, string ids)> CreateCollectionAsync(IEnumerable<LedgerForCreationDto> ledgerCollection);
        Task DeleteAsync(int ledgerId, bool trackChanges);
        Task UpdateAsync(int ledgerId, LedgerForUpdateDto ledger, bool trackChanges);
        Task<(LedgerForUpdateDto ledgerForUpdate, Ledger ledgerEntity)> GetLedgerForPatchAsync(int ledgerId, bool trackChanges);
        Task SaveChangesForPatchAsync(LedgerForUpdateDto ledgerToPatch, Ledger ledgerEntity);
    }
}