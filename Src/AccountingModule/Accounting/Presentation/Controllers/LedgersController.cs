using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Presentation.Controllers
{
    [Route("api/{companyId:int}/accounting/ledgers")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public LedgersController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetLedgers()
        {
            var ledgers = _service.LedgerService.GetAllLedgers(trackChanges: false);
            return Ok(ledgers);
        }

        [HttpGet("{id:int}", Name = "LedgerById")]
        public IActionResult GetLedger(int id)
        {
            var ledger = _service.LedgerService.GetLedger(id, trackChanges: false);
            return Ok(ledger);
        }

        [HttpPost]
        public IActionResult CreateLedger(int companyId, [FromBody] LedgerForCreationDto ledger)
        {
            if (ledger is null)
                return BadRequest("LedgerForCreationDto object is null");
            var createdLedger = _service.LedgerService.CreateLedger(ledger);
            return CreatedAtRoute("LedgerById", new { companyId, id = createdLedger.Id }, createdLedger);
        }

        [HttpGet("collection/({ids})", Name = "LedgerCollection")]
        public IActionResult GetLedgerCollection(int companyId, [ModelBinder(BinderType =typeof(ArrayModelBinder))]
        IEnumerable<int> ids)
        {
            var ledgers = _service.LedgerService.GetByIds(ids, trackChanges: false);
            return Ok(ledgers);
        }

        [HttpPost("collection")]
        public IActionResult CreateLedgerCollection(int companyId, [FromBody] IEnumerable<LedgerForCreationDto> ledgers)
        {
            var result = _service.LedgerService.CreateLedgerCollection(ledgers);
            return CreatedAtRoute("LedgerCollection", new { companyId, result.ids }, result.ledgers);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteLedger(int id)
        {
            _service.LedgerService.DeleteLedger(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateLedger(int id, [FromBody] LedgerForUpdateDto ledger)
        {
            if (ledger is null)
                return BadRequest("Ledger object is null");
            _service.LedgerService.UpdateLedger(id, ledger, trackChanges: true);
            return NoContent();
        }
    }
}
