using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IProductCodeRepository
    {
        IEnumerable<ProductCode> GetAll(bool trackChanges);
        ProductCode? Get(int CompanyId, bool trackChanges);
        void Create(ProductCode ProductCode);
        IEnumerable<ProductCode> GetByIds(IEnumerable<int> CompanyId, bool trackChanges);
        void Delete(ProductCode ProductCode);
    }
}
