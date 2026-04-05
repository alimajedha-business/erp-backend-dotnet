using FluentValidation;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.RequestValidators;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator() 
    {

    }
}
