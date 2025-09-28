using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Interfaces.Services;

namespace Warehouse.Application.Interfaces.Services
{
    public interface IServiceManager
    {
        IWarehouseTypeService WarehouseTypeService { get; }

        IWarehouseStockService WarehouseStockService { get; }

        IProductHierarchyService ProductHierarchyService { get; }

        IProductCodeService ProductCodeService { get; }

    }
}
