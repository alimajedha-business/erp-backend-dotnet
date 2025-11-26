using NGErp.Base.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Domain.Exceptions
{
    public class DomainNotFoundException : NotFoundException
    {
        public DomainNotFoundException(int domainId) :
            base("DomainNotFound", domainId)
        {
        }
    }
}
