using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Domain.Exceptions
{
    internal class DomainNotFoundException : NotFoundException
    {
        public DomainNotFoundException(int domainId) :
            base("DomainNotFound", domainId)
        {
        }
    }
}
