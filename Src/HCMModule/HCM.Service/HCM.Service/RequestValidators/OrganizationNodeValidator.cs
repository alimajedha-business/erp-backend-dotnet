using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.RequestValidators
{
    public class OrganizationNodeValidator : AbstractValidator<OrganizationNode>
    {
        private readonly IStringLocalizer<HCMResource> _localizer;
        public OrganizationNodeValidator(IStringLocalizer<HCMResource> localizer)
        {
            _localizer = localizer;

        }
    }
}