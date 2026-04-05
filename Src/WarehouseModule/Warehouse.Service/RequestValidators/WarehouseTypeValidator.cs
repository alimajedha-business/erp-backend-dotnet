using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class WarehouseTypeValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<WarehouseType>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
