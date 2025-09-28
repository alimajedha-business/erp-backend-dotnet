using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public sealed class WarehouseStockNotFoundException : NotFoundException
    {
        public WarehouseStockNotFoundException(int Id)
        : base($"The WarehouseStock with id: {Id} doesn't exist in the database.")
        {
        }
    }
}
