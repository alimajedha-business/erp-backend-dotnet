using FluentValidation;

namespace NGErp.Warehouse.Service.RequestValidators;

public class WarehouseValidator : AbstractValidator<Domain.Entities.Warehouse>
{
    public WarehouseValidator()
    {

    }
}
