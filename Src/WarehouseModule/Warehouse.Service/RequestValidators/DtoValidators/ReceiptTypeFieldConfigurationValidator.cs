using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateReceiptTypeFieldConfigurationValidator :
    AbstractValidator<CreateReceiptTypeFieldConfigurationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateReceiptTypeFieldConfigurationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.FieldDefinitionId)
            .NotEmpty()
            .WithMessage(
                _localizer["ReceiptTypeFieldConfiguration.FieldDefinition.NotEmpty"].Value
            );

        RuleFor(p => p.Exists)
            .NotNull()
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Exists.NotNull"].Value);

        RuleFor(p => p.IsRequired)
            .NotNull()
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.IsRequired.NotNull"].Value);

        RuleFor(p => p.Placement)
            .NotNull()
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Placement.NotNull"].Value)
            .Must(p => p is null || Enum.IsDefined(typeof(ReceiptConfiguredPlacement), p.Value))
            .WithMessage(_localizer["ReceiptTypeFieldConfiguration.Placement.Invalid"].Value);
    }
}
