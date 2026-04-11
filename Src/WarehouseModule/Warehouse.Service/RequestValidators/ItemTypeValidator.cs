using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateItemTypeValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<CreateItemTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
