using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreteWarehouseValidator : AbstractValidator<CreateWarehouseDto>
{
    public CreteWarehouseValidator()
    {

    }
}
