// Ignore Spelling: localizer

using Asp.Versioning;
using NGErp.General.Service.Interfaces.Services;
using NGErp.General.Service.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.API.Controllers
{
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-general")]
    [Route("api/v{version:apiVersion}/general/companies")]
    [ApiController]
    public class CompaniesController:ControllerBase
    {
        private readonly IGeneralServiceManager _service;
        private readonly IStringLocalizer<GeneralResource> _localizer;
        
        public CompaniesController(IGeneralServiceManager service, IStringLocalizer<GeneralResource> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCompany(int id)
        {
            var companyDto = await _service.CompanyService.GetCompanyAsync(id, false);
            return Ok(companyDto);
        }      
    }
}
