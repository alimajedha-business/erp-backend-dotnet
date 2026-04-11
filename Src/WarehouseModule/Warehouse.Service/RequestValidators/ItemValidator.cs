using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    public CreateItemValidator() 
    {

    }
}
