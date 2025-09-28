using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public sealed class ProductCodeNotFoundException : NotFoundException
    {
        public ProductCodeNotFoundException(int CompanyId)
        : base($"The  ProductCode with CompanyId: {CompanyId} doesn't exist in the database.")
        {
        }
    }
}
