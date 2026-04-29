using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateWarehouseTypeValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<CreateWarehouseTypeDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
