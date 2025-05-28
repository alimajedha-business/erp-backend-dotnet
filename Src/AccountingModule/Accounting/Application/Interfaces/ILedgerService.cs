using Accounting.Application.DTOs;
using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Interfaces
{
    public interface ILedgerService
    {
        IEnumerable<LedgerDto> GetAllLedgers(bool trackChanges);
        LedgerDto? GetLedger(int LedgerId, bool trackChanges);
        LedgerDto CreateLedger(LedgerForCreationDto ledger);
        IEnumerable<LedgerDto> GetByIds(IEnumerable<int> ids, bool trackChanges);
        (IEnumerable<LedgerDto> ledgers, string ids) CreateLedgerCollection(IEnumerable<LedgerForCreationDto> ledgerCollection);
    }
}