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

        RuleFor(p => p.Weigh)
            .GreaterThan(0)
            .When(p => p.Weigh.HasValue);

        RuleFor(p => p.Length)
            .GreaterThan(0)
            .When(p => p.Length.HasValue);

        RuleFor(p => p.Width)
            .GreaterThan(0)
            .When(p => p.Width.HasValue);

        RuleFor(p => p.Height)
            .GreaterThan(0)
            .When(p => p.Height.HasValue);

        RuleFor(p => p.CubeVolume)
            .GreaterThan(0)
            .When(p => p.CubeVolume.HasValue);

        RuleFor(p => p.PreferredMassUnitId)
            .NotEmpty()
            .When(p => p.Weigh.HasValue);

        RuleFor(p => p.PreferredLengthUnitId)
            .NotEmpty()
            .When(p => p.Length.HasValue || p.Width.HasValue || p.Height.HasValue);

        RuleFor(p => p.PreferredVolumeUnitId)
            .NotEmpty()
            .When(p => p.CubeVolume.HasValue);
    }
}
