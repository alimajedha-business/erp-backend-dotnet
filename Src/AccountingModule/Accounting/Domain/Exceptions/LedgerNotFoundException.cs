using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Exceptions
{
    public sealed class LedgerNotFoundException : NotFoundException
    {
        public LedgerNotFoundException(int ledgerId)
            : base("LedgerNotFound", ledgerId)
        {
        }
    }
}
