using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptLineValidator : AbstractValidator<CreateReceiptLineDto>
{
    public CreateReceiptLineValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.ItemId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLine.Item.NotEmpty"].Value);

        RuleFor(e => e.WarehouseLocationId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLine.WarehouseLocation.NotEmpty"].Value);

        RuleFor(e => e.Weight)
            .GreaterThan(0)
            .When(e => e.Weight.HasValue);

        RuleFor(e => e.Volume)
            .GreaterThan(0)
            .When(e => e.Volume.HasValue);

        RuleFor(e => e.PreferredMassUnitId)
            .NotEmpty()
            .When(e => e.Weight.HasValue);

        RuleFor(e => e.PreferredVolumeUnitId)
            .NotEmpty()
            .When(e => e.Volume.HasValue);

        RuleFor(e => e.ReceiptLineMeasurementValues)
            .NotNull()
            .WithMessage(localizer["ReceiptLine.MeasurementValues.NotEmpty"].Value)
            .Must(e => e is not null && e.Count != 0)
            .WithMessage(localizer["ReceiptLine.MeasurementValues.NotEmpty"].Value);

        RuleFor(e => e.ReceiptLineAttributeValues)
            .NotNull()
            .WithMessage(localizer["ReceiptLine.AttributeValues.NotNull"].Value);

        RuleFor(e => e.ReceiptFieldValues)
            .NotNull()
            .WithMessage(localizer["ReceiptLine.FieldValues.NotNull"].Value);

        RuleForEach(e => e.ReceiptLineAttributeValues)
            .SetValidator(new CreateReceiptLineAttributeValueValidator(localizer))
            .When(e => e.ReceiptLineAttributeValues is not null);

        RuleForEach(e => e.ReceiptLineMeasurementValues)
            .SetValidator(new CreateReceiptLineMeasurementValueValidator(localizer))
            .When(e => e.ReceiptLineMeasurementValues is not null);

        RuleForEach(e => e.ReceiptFieldValues)
            .SetValidator(new CreateReceiptFieldValueValidator(localizer))
            .When(e => e.ReceiptFieldValues is not null);
    }
}
