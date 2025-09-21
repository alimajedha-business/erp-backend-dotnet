using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public sealed class WarehouseTypeNotFoundException : NotFoundException
    {
        public WarehouseTypeNotFoundException(int Id)
        : base($"The WarehouseType with id: {Id} doesn't exist in the database.")
        {
        }
    }
}
