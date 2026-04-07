using FluentValidation;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.RequestValidators;

public class ItemAttributeValidator : AbstractValidator<ItemAttribute>
{
    public ItemAttributeValidator()
    {

    }
}
