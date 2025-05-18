using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        ILedgerRepository Ledger { get; }
        void Save();
    }
}
