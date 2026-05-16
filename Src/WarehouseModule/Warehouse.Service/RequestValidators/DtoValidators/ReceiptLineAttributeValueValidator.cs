using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptLineAttributeValueValidator :
    AbstractValidator<CreateReceiptLineAttributeValueDto>
{
    public CreateReceiptLineAttributeValueValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.ItemAttributeId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptLineAttributeValue.ItemAttribute.NotEmpty"].Value);
    }
}