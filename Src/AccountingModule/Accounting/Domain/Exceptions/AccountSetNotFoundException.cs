using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Exceptions
{
    internal class AccountSetNotFoundException : NotFoundException
    {
        public AccountSetNotFoundException(int accountSetId)
            : base("AccountSetNotFound", accountSetId)
        {
        }
    }
}
