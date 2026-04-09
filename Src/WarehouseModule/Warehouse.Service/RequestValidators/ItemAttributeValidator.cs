using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateItemAttributeValidator :
    AbstractValidator<CreateItemAttributeDto>
{
    public CreateItemAttributeValidator()
    {

    }
}
