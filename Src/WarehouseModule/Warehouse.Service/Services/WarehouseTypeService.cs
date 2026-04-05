using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseTypeRepository warehouseTypeRepository,
    IMapper mapper,
    IValidator<WarehouseType> validator,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        WarehouseType,
        WarehouseTypeDto,
        WarehouseTypeParameters,
        IWarehouseTypeRepository,
        WarehouseResource
    >(
        filterBuilder,
        warehouseTypeRepository,
        mapper,
        validator,
        localizer
    ),
    IWarehouseTypeService
{
    protected override string LocalizerKey => "WarehouseType";
}
