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

public class ItemTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IItemTypeRepository itemTypeRepository,
    IMapper mapper,
    IValidator<ItemType> validator,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        ItemType,
        ItemTypeDto,
        ItemTypeParameters,
        IItemTypeRepository,
        WarehouseResource
    >(
        filterBuilder,
        itemTypeRepository,
        mapper,
        validator,
        localizer
    ),
    IItemTypeService
{
    protected override string LocalizerKey => "ItemType";
}
