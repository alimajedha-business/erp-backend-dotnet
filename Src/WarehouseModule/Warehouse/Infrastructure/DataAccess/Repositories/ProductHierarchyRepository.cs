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
    public class ProductHierarchyRepository : RepositoryBase<ProductHierarchy>, IProductHierarchyRepository
    {
        public ProductHierarchyRepository(WarehouseDbContext context) : base(context)
        {
        }


        public IEnumerable<ProductHierarchy> GetAll(bool trackChanges) =>
             FindAll(trackChanges)
            .OrderBy(l => l.CompanyId)
            .ToList();

        public ProductHierarchy? Get(int CompanyId, bool trackChanges) =>
            FindByCondition(a => a.CompanyId.Equals(CompanyId), trackChanges)
            .SingleOrDefault();

        public new void Create(ProductHierarchy ProductHierarchy)
        {
            base.Create(ProductHierarchy);
        }

        public new void Delete(ProductHierarchy ProductHierarchy) => base.Delete(ProductHierarchy);

        public IEnumerable<ProductHierarchy> GetByIds(IEnumerable<int> CompanyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
