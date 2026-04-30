using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateWarehouseLocationValidator : AbstractValidator<CreateWarehouseLocationDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreateWarehouseLocationValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseLocation.Code.NotEmpty"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["WarehouseLocation.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["WarehouseLocation.Title.MinLength"].Value);
    }
}
