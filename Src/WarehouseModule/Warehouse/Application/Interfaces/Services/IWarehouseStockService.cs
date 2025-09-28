using Warehouse.Application.DTOs;
using Warehouse.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Warehouse.Interfaces.Services
{
    public interface IWarehouseStockService
    {
        IEnumerable<WarehouseStockDto> GetAll(bool trackChanges);
        WarehouseStockDto? Get(int Id, bool trackChanges);
        WarehouseStockDto Create(WarehouseStockForCreationDto WarehouseStock);
        IEnumerable<WarehouseStockDto> GetByIds(IEnumerable<int> ids, bool trackChanges);
        (IEnumerable<WarehouseStockDto> WarehouseStocks, string ids) CreateCollection(IEnumerable<WarehouseStockForCreationDto> WarehouseStockCollection);
        void Delete(int Id, bool trackChanges);
        void Update(int Id, WarehouseStockForUpdateDto WarehouseStock, bool trackChanges);
        (WarehouseStockForUpdateDto WarehouseStockForUpdate, WarehouseStock WarehouseStockEntity) GetWarehouseStockForPatch(int Id, bool trackChanges);
        void SaveChangesForPatch(WarehouseStockForUpdateDto WarehouseStockToPatch, WarehouseStock WarehouseStockEntity);
    }
}