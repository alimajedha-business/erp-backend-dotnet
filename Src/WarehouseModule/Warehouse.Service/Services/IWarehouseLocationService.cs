using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IWarehouseLocationService : IBaseService<
    WarehouseLocation,
    WarehouseLocationDto,
    WarehouseLocationParameters,
    IWarehouseLocationRepository,
    IWarehouseRepository
>
{
    Task<WarehouseLocationDto> CreateAsync(
        CreateWarehouseLocationDto createDto,
        CancellationToken ct
    );
}
