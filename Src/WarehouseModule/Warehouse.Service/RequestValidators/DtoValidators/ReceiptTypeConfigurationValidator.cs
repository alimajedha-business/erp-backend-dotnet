using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptTypeConfigurationValidator :
    AbstractValidator<CreateReceiptTypeConfigurationDto>
{
    public CreateReceiptTypeConfigurationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(p => p.ReceiptTypeId)
            .NotEmpty()
            .WithMessage(localizer["ReceiptTypeConfiguration.ReceiptType.NotEmpty"].Value);

        RuleFor(p => p.FieldConfigurations)
            .NotNull()
            .WithMessage(localizer["ReceiptTypeConfiguration.FieldConfigurations.NotEmpty"].Value)
            .Must(p => p is not null && p.Count != 0)
            .WithMessage(localizer["ReceiptTypeConfiguration.FieldConfigurations.NotEmpty"].Value);

        RuleForEach(p => p.FieldConfigurations)
            .SetValidator(new CreateReceiptTypeFieldConfigurationValidator(localizer));
    }
}
