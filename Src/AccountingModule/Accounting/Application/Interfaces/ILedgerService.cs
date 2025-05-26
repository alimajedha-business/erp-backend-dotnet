using Accounting.Application.DTOs;
using Accounting.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Application.Interfaces
{
    public interface ILedgerService
    {
        IEnumerable<LedgerDto> GetAllLedgers(bool trackChanges);
    }
}