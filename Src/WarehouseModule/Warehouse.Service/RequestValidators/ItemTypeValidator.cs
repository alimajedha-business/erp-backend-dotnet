using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class ItemTypeValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<ItemType>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
