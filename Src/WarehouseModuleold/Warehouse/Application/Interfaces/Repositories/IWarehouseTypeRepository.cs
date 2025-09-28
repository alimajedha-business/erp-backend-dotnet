using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IWarehouseTypeRepository
    {
        IEnumerable<WarehouseType> GetAll(bool trackChanges);
        WarehouseType? Get(int Id, bool trackChanges);
        void Create(WarehouseType WarehouseType);
        IEnumerable<WarehouseType> GetByIds(IEnumerable<int> Id, bool trackChanges);
        void Delete(WarehouseType WarehouseType);
    }
}
