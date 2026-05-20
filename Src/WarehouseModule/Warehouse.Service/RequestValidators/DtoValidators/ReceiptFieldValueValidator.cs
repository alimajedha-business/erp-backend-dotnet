using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptFieldValueValidator :
    AbstractValidator<CreateReceiptFieldValueDto>
{
    public CreateReceiptFieldValueValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(e => e.FieldDefinitionId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptFieldValue.FieldDefinition.NotEmpty"].Value);

        RuleFor(e => e)
            .Custom((dto, context) =>
            {
                var normalizationError = ReceiptFieldValueInputNormalizer.Normalize(dto);

                if (normalizationError is not null)
                    context.AddFailure(nameof(CreateReceiptFieldValueDto.Value), normalizationError);
            });
    }
}
