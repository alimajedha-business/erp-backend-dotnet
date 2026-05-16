using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptLineMeasurementValueValidator :
    AbstractValidator<CreateReceiptLineMeasurementValueDto>
{
    public CreateReceiptLineMeasurementValueValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.ItemUnitOfMeasurementId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLineMeasurementValue.ItemUnitOfMeasurement.NotEmpty"].Value);

        RuleFor(e => e.Quantity)
            .GreaterThan(0)
            .WithMessage(localizer["ReceiptLine.Quantity.Positive"].Value);
    }
}
