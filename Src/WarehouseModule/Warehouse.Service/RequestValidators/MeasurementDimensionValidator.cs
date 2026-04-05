using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class MeasurementDimensionValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<MeasurementDimension>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}
