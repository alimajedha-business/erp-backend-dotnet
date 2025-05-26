using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Presentation.Controllers
{
    [Route("api/Accounting/Ledgers")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LedgerController(IServiceManager service) => _service = service;
        [HttpGet]
        public IActionResult GetLedgers()
        {
            try
            {
                var ledgers =
                _service.LedgerService.GetAllLedgers(trackChanges: false);
                return Ok(ledgers);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
