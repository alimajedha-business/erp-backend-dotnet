using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseRepository warehouseRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer

) : BaseServiceWithCompany<
        Domain.Entities.Warehouse,
        WarehouseDto,
        WarehouseListDto,
        WarehouseParameters,
        IWarehouseRepository,
        WarehouseResource
    >(
        filterBuilder,
        warehouseRepository,
        companyService,
        mapper,
        localizer
    ),
    IWarehouseService
{
    protected override string LocalizerKey => "Warehouse";
}
