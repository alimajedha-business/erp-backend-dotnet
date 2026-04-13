using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreteWarehouseValidator : AbstractValidator<CreateWarehouseDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public CreteWarehouseValidator(
        IStringLocalizer<WarehouseResource> localizer
    )
    {
        _localizer = localizer;

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Code.NotEmpty"].Value);

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Warehouse.Title.MinLength"].Value);

        RuleFor(p => p.WarehouseTypeId)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.Type.NotEmpty"].Value);

        RuleFor(p => p.CompanyUnitId)
            .NotEmpty()
            .WithMessage(_localizer["Warehouse.CompanyUnit.NotEmpty"].Value);
    }
}
