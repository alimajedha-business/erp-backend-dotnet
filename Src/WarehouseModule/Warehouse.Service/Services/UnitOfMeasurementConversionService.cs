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

public class UnitOfMeasurementConversionService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementConversionRepository unitOfMeasurementConversionRepository,
    IMapper mapper,
    IValidator<UnitOfMeasurementConversion> validator,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        UnitOfMeasurementConversion,
        UnitOfMeasurementConversionDto,
        UnitOfMeasurementConversionListDto,
        UnitOfMeasurementConversionParameters,
        IUnitOfMeasurementConversionRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementConversionRepository,
        mapper,
        validator,
        localizer
    ),
    IUnitOfMeasurementConversionService
{
    protected override string LocalizerKey => "UnitOfMeasurementConversion";
}