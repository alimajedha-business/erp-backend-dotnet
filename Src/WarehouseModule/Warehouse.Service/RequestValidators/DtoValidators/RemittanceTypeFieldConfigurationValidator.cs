using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateRemittanceTypeFieldConfigurationValidator :
    AbstractValidator<CreateRemittanceTypeFieldConfigurationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateRemittanceTypeFieldConfigurationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.FieldDefinitionId)
            .NotEmpty()
            .WithMessage(
                _localizer["RemittanceTypeFieldConfiguration.FieldDefinition.NotEmpty"].Value
            );

        RuleFor(p => p.Exists)
            .NotNull()
            .WithMessage(_localizer["RemittanceTypeFieldConfiguration.Exists.NotNull"].Value);

        RuleFor(p => p.IsRequired)
            .NotNull()
            .WithMessage(_localizer["RemittanceTypeFieldConfiguration.IsRequired.NotNull"].Value);

        RuleFor(p => p.Placement)
            .NotNull()
            .WithMessage(_localizer["RemittanceTypeFieldConfiguration.Placement.NotNull"].Value)
            .Must(p => p is null || Enum.IsDefined(typeof(RemittanceConfiguredPlacement), p.Value))
            .WithMessage(_localizer["RemittanceTypeFieldConfiguration.Placement.Invalid"].Value);
    }
}
