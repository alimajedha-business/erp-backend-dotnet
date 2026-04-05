using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class UnitOfMeasurementValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<UnitOfMeasurement>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
