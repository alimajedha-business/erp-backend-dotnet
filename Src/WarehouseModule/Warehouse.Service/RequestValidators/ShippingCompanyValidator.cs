using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators;

public class ShippingCompanyValidator : AbstractValidator<ShippingCompany>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;

    public ShippingCompanyValidator(IStringLocalizer<WarehouseResource> localizer)
    {
        _localizer = localizer;

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["ShippingCompany.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["ShippingCompany.Title.MinLength"].Value);
    }
}
