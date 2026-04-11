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

public class InventoryMovementTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IInventoryMovementTypeRepository inventoryMovementTypeRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        InventoryMovementType,
        InventoryMovementTypeDto,
        InventoryMovementTypeParameters,
        IInventoryMovementTypeRepository,
        WarehouseResource
    >(
        filterBuilder,
        inventoryMovementTypeRepository,
        companyService,
        mapper,
        localizer
    ),
    IInventoryMovementTypeService
{
    protected override string LocalizerKey => "InventoryMovementType";
}
