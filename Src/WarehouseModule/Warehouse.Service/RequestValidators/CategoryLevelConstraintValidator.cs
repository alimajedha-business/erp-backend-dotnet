using FluentValidation;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CategoryLevelConstraintValidator : AbstractValidator<CategoryLevelConstraint>
{
    public CategoryLevelConstraintValidator()
    {
        
    }
}
