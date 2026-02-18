using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class UnitOfMeasurementConversionService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementConversionRepository unitOfMeasurementConversionRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        UnitOfMeasurementConversion,
        UnitOfMeasurementConversionDto,
        UnitOfMeasurementConversionListDto,
        UnitOfMeasurementConversionParameters,
        IUnitOfMeasurementConversionRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementConversionRepository,
        companyService,
        mapper,
        localizer
    ),
    IUnitOfMeasurementConversionService
{
    protected override string LocalizerKey => "UnitOfMeasurementConversion";
}