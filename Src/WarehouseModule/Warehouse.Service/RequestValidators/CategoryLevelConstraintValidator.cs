using FluentValidation;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateCategoryLevelConstraintValidator : 
    AbstractValidator<CreateCategoryLevelConstraintDto>
{
    public CreateCategoryLevelConstraintValidator()
    {
        
    }
}
