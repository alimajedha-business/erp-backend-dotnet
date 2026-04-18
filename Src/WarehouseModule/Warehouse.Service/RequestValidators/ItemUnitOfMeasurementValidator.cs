using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateItemUnitOfMeasurementValidator :
    AbstractValidator<CreateItemUnitOfMeasurementDto>
{
    public CreateItemUnitOfMeasurementValidator()
    {

    }
}
