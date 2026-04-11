using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class ItemUnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IItemUnitOfMeasurementRepository itemUomRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        ItemUnitOfMeasurement,
        ItemUnitOfMeasurementDto,
        ItemUnitOfMeasurementParameters,
        IItemUnitOfMeasurementRepository,
        WarehouseResource
    >(
        filterBuilder,
        itemUomRepository,
        mapper,
        localizer
    ),
    IItemUnitOfMeasurementService
{
    protected override string LocalizerKey => "ItemUnitOfMeasurement";
}
