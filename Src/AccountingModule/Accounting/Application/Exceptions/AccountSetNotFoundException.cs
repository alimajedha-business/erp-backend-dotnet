using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Exceptions
{
    internal class AccountSetNotFoundException : NotFoundException
    {
        public AccountSetNotFoundException(int AccountSetId)
        : base($"The AccountSet with id: {AccountSetId} doesn't exist in the database.")
        {
        }
    }
}
