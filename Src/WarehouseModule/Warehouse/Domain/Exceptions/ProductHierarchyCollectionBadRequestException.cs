using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public sealed class ProductHierarchyCollectionBadRequestException : BadRequestException
    {
        public ProductHierarchyCollectionBadRequestException() : base("ProductHierarchy collection sent from a client is null.")
        { }
    }
}
