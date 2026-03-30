using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseTypeRepository warehouseTypeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        Domain.Entities.WarehouseType,
        DTOs.WarehouseTypeDto,
        RequestFeatures.WarehouseTypeParameters,
        IWarehouseTypeRepository,
        WarehouseResource
    >(
        filterBuilder,
        warehouseTypeRepository,
        mapper,
        localizer
    ),
    IWarehouseTypeService
{
    protected override string LocalizerKey => "WarehouseType";
}
