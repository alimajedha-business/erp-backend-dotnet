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
    public class ProductCodeRepository : RepositoryBase<ProductCode>, IProductCodeRepository
    {
        public ProductCodeRepository(IMainDbContext context) : base(context)
        {
        }


        public IEnumerable<ProductCode> GetAll(bool trackChanges) =>
             FindAll(trackChanges)
            .OrderBy(l => l.CompanyId)
            .ToList();

        public ProductCode? Get(int CompanyId, bool trackChanges) =>
            FindByCondition(a => a.CompanyId.Equals(CompanyId), trackChanges)
            .SingleOrDefault();

        public new void Create(ProductCode ProductCode)
        {
            base.Create(ProductCode);
        }

        public new void Delete(ProductCode ProductCode) => base.Delete(ProductCode);

        public IEnumerable<ProductCode> GetByIds(IEnumerable<int> CompanyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
