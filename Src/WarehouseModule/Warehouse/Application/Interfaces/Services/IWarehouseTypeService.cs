using Warehouse.Application.DTOs;
using Warehouse.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Warehouse.Interfaces.Services
{
    public interface IWarehouseTypeService
    {
        IEnumerable<WarehouseTypeDto> GetAll(bool trackChanges);
        WarehouseTypeDto? Get(int Id, bool trackChanges);
        WarehouseTypeDto Create(WarehouseTypeForCreationDto WarehouseType);
        IEnumerable<WarehouseTypeDto> GetByIds(IEnumerable<int> ids, bool trackChanges);
        (IEnumerable<WarehouseTypeDto> WarehouseTypes, string ids) CreateCollection(IEnumerable<WarehouseTypeForCreationDto> WarehouseTypeCollection);
        void Delete(int Id, bool trackChanges);
        void Update(int Id, WarehouseTypeForUpdateDto WarehouseType, bool trackChanges);
        (WarehouseTypeForUpdateDto WarehouseTypeForUpdate, WarehouseType WarehouseTypeEntity) GetWarehouseTypeForPatch(int Id, bool trackChanges);
        void SaveChangesForPatch(WarehouseTypeForUpdateDto WarehouseTypeToPatch, WarehouseType WarehouseTypeEntity);
    }
}