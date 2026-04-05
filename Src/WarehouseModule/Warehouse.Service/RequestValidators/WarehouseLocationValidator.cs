using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class WarehouseLocationValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<WarehouseLocation>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
