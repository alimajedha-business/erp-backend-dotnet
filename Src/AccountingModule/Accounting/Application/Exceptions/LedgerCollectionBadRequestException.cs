using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Exceptions
{
    public sealed class LedgerCollectionBadRequestException : BadRequestException
    {
        public LedgerCollectionBadRequestException(): base("Ledger collection sent from a client is null.")
        { }
    }
}
