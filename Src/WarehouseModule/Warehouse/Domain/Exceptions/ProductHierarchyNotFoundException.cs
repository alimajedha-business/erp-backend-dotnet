using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public sealed class ProductHierarchyNotFoundException : NotFoundException
    {
        public ProductHierarchyNotFoundException(int CompanyId)
        : base($"The  ProductHierarchy with CompanyId: {CompanyId} doesn't exist in the database.")
        {
        }
    }
}
