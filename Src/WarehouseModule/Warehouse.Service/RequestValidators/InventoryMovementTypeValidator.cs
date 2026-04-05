using FluentValidation;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.RequestValidators;

public class InventoryMovementTypeValidator : AbstractValidator<InventoryMovementType>
{
    public InventoryMovementTypeValidator()
    {

    }
}
