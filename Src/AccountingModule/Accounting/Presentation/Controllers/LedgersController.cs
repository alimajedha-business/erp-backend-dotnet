using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morcatko.AspNetCore.JsonMergePatch;
using System.ComponentModel.Design;

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
            var ledgers = _service.LedgerService.GetAll(trackChanges: false);
            return Ok(ledgers);
        }

        [HttpGet("{id:int}", Name = "LedgerById")]
        public IActionResult GetLedger(int id)
        {
            var ledger = _service.LedgerService.Get(id, trackChanges: false);
            return Ok(ledger);
        }

        [HttpPost]
        public IActionResult CreateLedger(int companyId, [FromBody] LedgerForCreationDto ledger)
        {
            if (ledger is null)
                return BadRequest("LedgerForCreationDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var createdLedger = _service.LedgerService.Create(ledger);
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
            var result = _service.LedgerService.CreateCollection(ledgers);
            return CreatedAtRoute("LedgerCollection", new { companyId, result.ids }, result.ledgers);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteLedger(int id)
        {
            _service.LedgerService.Delete(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateLedger(int id, [FromBody] LedgerForUpdateDto ledger)
        {
            if (ledger is null)
                return BadRequest("Ledger object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.LedgerService.Update(id, ledger, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public IActionResult PatchLedger(int id, [FromBody] JsonMergePatchDocument<LedgerForUpdateDto> ledgerPatch)
        {
            if (ledgerPatch is null)
            {
                return BadRequest("Ledger object is null.");
            }
            var result = _service.LedgerService.GetLedgerForPatch(id, trackChanges: true);
            ledgerPatch.ApplyTo(result.ledgerForUpdate);
            _service.LedgerService.SaveChangesForPatch(result.ledgerForUpdate, result.ledgerEntity);

            return NoContent();
        }
    }
}
