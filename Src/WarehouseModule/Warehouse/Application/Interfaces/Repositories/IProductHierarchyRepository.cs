using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IProductHierarchyRepository
    {
        IEnumerable<ProductHierarchy> GetAll(bool trackChanges);
        ProductHierarchy? Get(int CompanyId, bool trackChanges);
        void Create(ProductHierarchy ProductHierarchy);
        IEnumerable<ProductHierarchy> GetByIds(IEnumerable<int> CompanyId, bool trackChanges);
        void Delete(ProductHierarchy ProductHierarchy);
    }
}
