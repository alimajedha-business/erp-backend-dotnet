using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;


namespace NGErp.HCM.API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-hcm")]
    [Route("api/v{version:apiVersion}/{companyId:guid}/hcm/departments/")]
    //[JwtAuthorize]
    public class DepartmentController(
        IDepartmentService departmentService,
        IAdvancedFilterBuilder filterBuilder) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;

        [HttpPost("search/")]
        [SkipModelValidation]
        public async Task<IActionResult> GetWithSearch(
            Guid companyId,
            [FromQuery] DepartmentParameters departmentParameters,
            [FromBody] FilterNodeDto? filterNodeDto)
        {
            var requestAdvancedFilters = _filterBuilder.Build<Department>(filterNodeDto);

            var result = await _departmentService.GetAllDepartmentsAsync(
                companyId,
                departmentParameters,
                requestAdvancedFilters
            );

            return Ok(result);
        }


    }
}
