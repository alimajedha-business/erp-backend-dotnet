using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IWarehouseRepositoryManager
    {
        IWarehouseTypeRepository WarehouseType { get; }
        IWarehouseStockRepository WarehouseStock { get; }

        IProductHierarchyRepository ProductHierarchy { get; }

        IProductCodeRepository ProductCode { get; }

        void Save();
    }
}
