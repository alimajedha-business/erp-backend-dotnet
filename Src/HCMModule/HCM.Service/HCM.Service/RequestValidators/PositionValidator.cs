using FluentValidation;

using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.RequestValidators;

public class PositionValidator : AbstractValidator<Position>
{
    public PositionValidator()
    {

    }
}
