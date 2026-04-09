using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateWarehouseLocationValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<CreateWarehouseLocationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
