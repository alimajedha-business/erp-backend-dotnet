using Asp.Versioning;
using General.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Presentation.Controllers
{
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-general")]
    [Route("api/v{version:apiVersion}/general/[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public DomainsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetDomain(int id)
        {

            var domain = await _service.DomainService.GetDomainAsync(id, false);
            return Ok(domain);
        }

    }
}
