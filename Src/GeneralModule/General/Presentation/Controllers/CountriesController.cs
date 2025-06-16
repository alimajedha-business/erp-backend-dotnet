using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace General.Presentation.Controllers
{
    [Route("api/general/countries")]
    [ApiController]
    internal class CountriesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CountriesController(IServiceManager service) => _service = service;


        [HttpGet]
        public IActionResult GetCountries(int ledgerId)
        {
            var accountSets = _service.CountryService.GetAll(trackChanges: false);
            return Ok(accountSets);
        }
    }
}
