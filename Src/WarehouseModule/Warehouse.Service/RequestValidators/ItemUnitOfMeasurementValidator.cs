using FluentValidation;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.RequestValidators;

public class ItemUnitOfMeasurementValidator :
    AbstractValidator<ItemUnitOfMeasurement>
{
    public ItemUnitOfMeasurementValidator()
    {

    }
}
