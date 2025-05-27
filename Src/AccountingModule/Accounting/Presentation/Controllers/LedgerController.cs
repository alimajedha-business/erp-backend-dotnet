using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Presentation.Controllers
{
    [Route("api/accounting/ledgers")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LedgerController(IServiceManager service) => _service = service;
        [HttpGet]
        public IActionResult GetLedgers()
        {
            var ledgers = _service.LedgerService.GetAllLedgers(trackChanges: false);
            return Ok(ledgers);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetLedger(int id)
        {
            var ledger = _service.LedgerService.GetLedger(id, trackChanges: false);
            return Ok(ledger);
        }
    }
}
