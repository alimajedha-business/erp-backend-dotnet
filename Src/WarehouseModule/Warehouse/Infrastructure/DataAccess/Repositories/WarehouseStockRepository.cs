using Common.Application.Interfaces;
using Common.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.DataAccess.Repositories
{
    public class WarehouseStockRepository : RepositoryBase<WarehouseStock>, IWarehouseStockRepository
    {
        public WarehouseStockRepository(IMainDbContext context) : base(context)
        {
        }


        public IEnumerable<WarehouseStock> GetAll(bool trackChanges) =>
             FindAll(trackChanges)
            .OrderBy(l => l.Code)
            .ToList();

        public WarehouseStock? Get(int Id,  bool trackChanges) =>
            FindByCondition(a => a.Code.Equals(Id) , trackChanges)
            .SingleOrDefault();

        public new void Create(WarehouseStock warehouseStock)
        {
           base.Create(warehouseStock);
        }

        public new void Delete(WarehouseStock warehouseStock) => base.Delete(warehouseStock);

        public IEnumerable<WarehouseStock> GetByIds(IEnumerable<int> Id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
