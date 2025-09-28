using Warehouse.Application.DTOs;
using Warehouse.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Warehouse.Interfaces.Services
{
    public interface IProductCodeService
    {
        IEnumerable<ProductCodeDto> GetAll(bool trackChanges);
        ProductCodeDto? Get(int CompanyId, bool trackChanges);
        ProductCodeDto Create(ProductCodeForCreationDto ProductCode);
        IEnumerable<ProductCodeDto> GetByIds(IEnumerable<int> CompanyIds, bool trackChanges);
        (IEnumerable<ProductCodeDto> ProductCodes, string CompanyIds) CreateCollection(IEnumerable<ProductCodeForCreationDto> ProductCodeCollection);
        void Delete(int CompanyId, bool trackChanges);
        void Update(int CompanyId, ProductCodeForUpdateDto ProductCode, bool trackChanges);
        (ProductCodeForUpdateDto ProductCodeForUpdate, ProductCode ProductCodeEntity) GetProductCodeForPatch(int CompanyId, bool trackChanges);
        void SaveChangesForPatch(ProductCodeForUpdateDto ProductCodeToPatch, ProductCode ProductCodeEntity);
    }
}