using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateAttributeValidator : AbstractValidator<CreateAttributeDto>
{
    public CreateAttributeValidator()
    {

    }
}
