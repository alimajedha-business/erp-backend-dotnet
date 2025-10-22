using Asp.Versioning;
using HCM.Application.DTOs;
using HCM.Application.Interfaces.Services;
using HCM.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Presentation.ActionFilters;

namespace HCM.Presentation.Controllers
{
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-hcm")]
    [Route("api/v{version:apiVersion}/{companyId}/hcm/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IHCMServiceManager _service;
        private readonly IStringLocalizer<HCMResource> _localizer;

        public DepartmentsController(IHCMServiceManager service, IStringLocalizer<HCMResource> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDepartmentForCompany(int companyId, [FromBody] DepartmentForCreationDto department)
        {
            var departmentToReturn = await _service.DepartmentService.CreateDepartmentForCompanyAsync(companyId, department,
                trackChanges: false);

            return CreatedAtRoute("GetDepartmentForCompany", new { companyId, id = departmentToReturn.Id },
                departmentToReturn);
        }
    }
}
