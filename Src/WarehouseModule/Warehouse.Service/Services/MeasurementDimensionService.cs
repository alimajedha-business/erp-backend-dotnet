using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class MeasurementDimensionService(
    IAdvancedFilterBuilder filterBuilder,
    IMeasurementDimensionRepository measurementDimensionRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        MeasurementDimension,
        MeasurementDimensionDto,
        MeasurementDimensionParameters,
        IMeasurementDimensionRepository,
        WarehouseResource
    >(
        filterBuilder,
        measurementDimensionRepository,
        mapper,
        localizer
    ),
    IMeasurementDimensionService
{
    protected override string LocalizerKey => "MeasurementDimension";
}
