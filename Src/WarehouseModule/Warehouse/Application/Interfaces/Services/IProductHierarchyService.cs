using Warehouse.Application.DTOs;
using Warehouse.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Warehouse.Interfaces.Services
{
    public interface IProductHierarchyService
    {
        IEnumerable<ProductHierarchyDto> GetAll(bool trackChanges);
        ProductHierarchyDto? Get(int CompanyId, bool trackChanges);
        ProductHierarchyDto Create(ProductHierarchyForCreationDto ProductHierarchy);
        IEnumerable<ProductHierarchyDto> GetByIds(IEnumerable<int> CompanyIds, bool trackChanges);
        (IEnumerable<ProductHierarchyDto> ProductHierarchies, string CompanyIds) CreateCollection(IEnumerable<ProductHierarchyForCreationDto> ProductHierarchyCollection);
        void Delete(int CompanyId, bool trackChanges);
        void Update(int CompanyId, ProductHierarchyForUpdateDto ProductHierarchy, bool trackChanges);
        (ProductHierarchyForUpdateDto ProductHierarchyForUpdate, ProductHierarchy ProductHierarchyEntity) GetProductHierarchyForPatch(int CompanyId, bool trackChanges);
        void SaveChangesForPatch(ProductHierarchyForUpdateDto ProductHierarchyToPatch, ProductHierarchy ProductHierarchyEntity);
    }
}