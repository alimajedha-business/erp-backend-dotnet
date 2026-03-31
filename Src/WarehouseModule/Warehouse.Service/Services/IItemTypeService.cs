using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IItemTypeService : IBaseService<
    ItemType,
    ItemTypeDto,
    ItemTypeParameters,
    IItemTypeRepository,
    WarehouseService
>
{ }
