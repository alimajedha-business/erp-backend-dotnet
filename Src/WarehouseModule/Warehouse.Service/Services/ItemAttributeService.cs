using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class ItemAttributeService(
    IAdvancedFilterBuilder filterBuilder,
    IItemAttributeRepository itemAttributeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        ItemAttribute,
        ItemAttributeDto,
        ItemAttributeParameters,
        IItemAttributeRepository,
        WarehouseResource
    >(
        filterBuilder,
        itemAttributeRepository,
        mapper,
        localizer
    ),
    IItemAttributeService
{
    protected override string LocalizerKey => "ItemAttribute";
}
