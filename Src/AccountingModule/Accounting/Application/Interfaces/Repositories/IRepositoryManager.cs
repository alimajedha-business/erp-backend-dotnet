using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        ILedgerRepository Ledger { get; }
        IAccountSetRepository AccountSet { get; }
        void Save();
    }
}
