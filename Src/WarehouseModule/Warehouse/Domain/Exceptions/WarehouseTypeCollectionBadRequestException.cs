using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public sealed class WarehouseTypeCollectionBadRequestException : BadRequestException
    {
        public WarehouseTypeCollectionBadRequestException() : base("WarehouseType collection sent from a client is null.")
        { }
    }
}
