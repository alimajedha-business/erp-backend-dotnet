using Accounting.Application.DTOs;

namespace Accounting.Application.Interfaces
{
    public interface ILedgerService
    {
        Task<IEnumerable<LedgerDTo>> GetAllAsync();
        Task AddAsync(LedgerDTo dto);
    }
}