using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateItemUnitOfMeasurementValidator :
    AbstractValidator<CreateItemUnitOfMeasurementDto>
{
    public CreateItemUnitOfMeasurementValidator()
    {
        RuleFor(p => p.UnitOfMeasurementId)
            .NotEmpty();

        RuleFor(p => p.UnitOrder)
            .GreaterThan(0);
    }
}
