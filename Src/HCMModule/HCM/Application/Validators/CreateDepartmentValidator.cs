using FluentValidation;
using General.Application.Interfaces.Services;
using HCM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<DepartmentForCreationDto>
    {   private readonly IGeneralServiceManager _generalServiceManager;
        public CreateDepartmentValidator(IGeneralServiceManager generalServiceManager)
        {
          _generalServiceManager = generalServiceManager;

            RuleFor(x => x.Code).NotEmpty().WithMessage("no empty code");
        }
    }
}
