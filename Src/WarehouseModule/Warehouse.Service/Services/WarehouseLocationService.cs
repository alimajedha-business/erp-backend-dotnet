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

public class WarehouseLocationService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseLocationRepository locationRepository,
    IMapper mapper,
    IValidator<WarehouseLocation> validator,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        WarehouseLocation,
        WarehouseLocationDto,
        WarehouseLocationParameters,
        IWarehouseLocationRepository,
        WarehouseResource
    >(
        filterBuilder,
        locationRepository,
        mapper,
        validator,
        localizer
    ),
    IWarehouseLocationService
{
    protected override string LocalizerKey => "WarehouseLocation";
}
