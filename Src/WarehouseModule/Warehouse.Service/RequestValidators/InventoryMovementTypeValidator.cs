using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateInventoryMovementTypeValidator :
    AbstractValidator<CreateInventoryMovementTypeDto>
{
    public CreateInventoryMovementTypeValidator()
    {

    }
}
