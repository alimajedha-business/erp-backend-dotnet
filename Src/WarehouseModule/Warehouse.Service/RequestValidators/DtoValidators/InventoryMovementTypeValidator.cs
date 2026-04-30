using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateInventoryMovementTypeValidator :
    AbstractValidator<CreateInventoryMovementTypeDto>
{
    public CreateInventoryMovementTypeValidator()
    {

    }
}
