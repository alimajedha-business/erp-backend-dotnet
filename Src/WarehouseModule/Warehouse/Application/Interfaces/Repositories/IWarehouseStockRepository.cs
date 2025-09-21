using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IWarehouseStockRepository
    {
        IEnumerable<WarehouseStock> GetAll(bool trackChanges);
        WarehouseStock? Get(int Id, bool trackChanges);
        void Create(WarehouseStock WarehouseStock);
        IEnumerable<WarehouseStock> GetByIds(IEnumerable<int> Id, bool trackChanges);
        void Delete(WarehouseStock WarehouseStock);
    }
}
