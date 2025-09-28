using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Entities;
using Common.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.DataAccess.Repositories
{
    public class WarehouseTypeRepository : RepositoryBase<WarehouseType>, IWarehouseTypeRepository
    {
        public WarehouseTypeRepository(WarehouseDbContext context) : base(context)
        {
        }


        public IEnumerable<WarehouseType> GetAll(bool trackChanges) =>
             FindAll(trackChanges)
            .OrderBy(l => l.Code)
            .ToList();

        public WarehouseType? Get(int Id,  bool trackChanges) =>
            FindByCondition(a => a.Code.Equals(Id) , trackChanges)
            .SingleOrDefault();

        public new void Create(WarehouseType warehouseType)
        {
           base.Create(warehouseType);
        }

        public new void Delete(WarehouseType warehouseType) => base.Delete(warehouseType);

        public IEnumerable<WarehouseType> GetByIds(IEnumerable<int> Id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
