using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateCategoryLevelConstraintValidator : 
    AbstractValidator<CreateCategoryLevelConstraintDto>
{
    public CreateCategoryLevelConstraintValidator()
    {
        
    }
}
