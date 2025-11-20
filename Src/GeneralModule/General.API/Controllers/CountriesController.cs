using Asp.Versioning;
using Common.Application.RequestParameters;
using General.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace General.Presentation.Controllers
{
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-general")]
    [Route("api/v{version:apiVersion}/general/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IGeneralServiceManager _service;
        public CountriesController(IGeneralServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCountries([FromQuery] CountryParameters countryParameters)
        {
            var pagedResult = _service.CountryService.GetAll(countryParameters, trackChanges: false);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagedResult.metaData);
            return Ok(pagedResult.countries);
        }
    }
}
