using Base.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Domain.Exceptions
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(int companyId) :
            base("CompanyNotFound", companyId)
        {
        }
    }
}
