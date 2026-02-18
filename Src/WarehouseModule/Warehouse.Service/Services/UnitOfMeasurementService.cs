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

public class UnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementRepository unitOfMeasurementRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        UnitOfMeasurement,
        UnitOfMeasurementDto,
        UnitOfMeasurementParameters,
        IUnitOfMeasurementRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementRepository,
        companyService,
        mapper,
        localizer
    ),
    IUnitOfMeasurementService
{
    protected override string LocalizerKey => "UnitOfMeasurement";
}
