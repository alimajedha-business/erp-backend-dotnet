using General.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Common.Application.RequestParameters;
using System.Text.Json;

namespace General.Presentation.Controllers
{
    [Route("api/general/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CountriesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCountries([FromQuery] CountryParameters countryParameters)
        {
            var pagedResult = _service.CountryService.GetAll(countryParameters,trackChanges: false);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagedResult.metaData);
            return Ok(pagedResult.countries);
        }
    }
}
