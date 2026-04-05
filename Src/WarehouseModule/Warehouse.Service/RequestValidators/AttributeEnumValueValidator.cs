using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class AttributeEnumValueValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<AttributeEnumValue>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
