using FluentValidation;

using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Service.RequestValidators;

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {

    }
}
