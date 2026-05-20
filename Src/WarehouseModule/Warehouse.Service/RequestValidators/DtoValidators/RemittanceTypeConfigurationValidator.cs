using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateRemittanceTypeConfigurationValidator :
    AbstractValidator<CreateRemittanceTypeConfigurationDto>
{
    public CreateRemittanceTypeConfigurationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        RuleFor(p => p.RemittanceTypeId)
            .NotEmpty()
            .WithMessage(localizer["RemittanceTypeConfiguration.RemittanceType.NotEmpty"].Value);

        RuleFor(p => p.FieldConfigurations)
            .NotNull()
            .WithMessage(localizer["RemittanceTypeConfiguration.FieldConfigurations.NotEmpty"].Value)
            .Must(p => p is not null && p.Count != 0)
            .WithMessage(localizer["RemittanceTypeConfiguration.FieldConfigurations.NotEmpty"].Value);

        RuleForEach(p => p.FieldConfigurations)
            .SetValidator(new CreateRemittanceTypeFieldConfigurationValidator(localizer));
    }
}
