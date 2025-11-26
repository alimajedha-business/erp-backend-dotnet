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

namespace NGErp.General.Presentation.Controllers
{
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-general")]
    [Route("api/v{version:apiVersion}/general/domains")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly IGeneralServiceManager _service;
        private readonly IStringLocalizer<GeneralResource> _localizer;
        public DomainsController(IGeneralServiceManager service, IStringLocalizer<GeneralResource> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetDomain(int id)
        {
            var domain = await _service.DomainService.GetDomainAsync(id, false);
            return Ok(domain);
        }
    }
}
