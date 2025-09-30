using Accounting.Application.DTOs;
using Accounting.Application.Interfaces.Services;
using Accounting.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morcatko.AspNetCore.JsonMergePatch;
using System.ComponentModel.Design;

namespace Accounting.Presentation.Controllers
{
    [Route(RouteConstants.BaseUrl + "/{companyId:int}/accounting/ledgers")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public LedgersController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetLedgers()
        {
            var ledgers = await _service.LedgerService.GetAllAsync(trackChanges: false);
            return Ok(ledgers);
        }

        [HttpGet("{id:int}", Name = "LedgerById")]
        public async Task<IActionResult> GetLedger(int id)
        {
            var ledger = await _service.LedgerService.GetAsync(id, trackChanges: false);
            return Ok(ledger);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLedger(int companyId, [FromBody] LedgerForCreationDto ledger)
        {
            if (ledger is null)
                return BadRequest("LedgerForCreationDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var createdLedger = await _service.LedgerService.CreateAsync(ledger);
            return CreatedAtRoute("LedgerById", new { companyId, id = createdLedger.Id }, createdLedger);
        }

        [HttpGet("collection/({ids})", Name = "LedgerCollection")]
        public async Task<IActionResult> GetLedgerCollection(int companyId, [ModelBinder(BinderType =typeof(ArrayModelBinder))]
        IEnumerable<int> ids)
        {
            var ledgers = await _service.LedgerService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(ledgers);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateLedgerCollection(int companyId, [FromBody] IEnumerable<LedgerForCreationDto> ledgers)
        {
            var result = await _service.LedgerService.CreateCollectionAsync(ledgers);
            return CreatedAtRoute("LedgerCollection", new { companyId, result.ids }, result.ledgers);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLedger(int id)
        {
            await _service.LedgerService.DeleteAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLedger(int id, [FromBody] LedgerForUpdateDto ledger)
        {
            if (ledger is null)
                return BadRequest("Ledger object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _service.LedgerService.UpdateAsync(id, ledger, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public async Task<IActionResult> PatchLedger(int id, [FromBody] JsonMergePatchDocument<LedgerForUpdateDto> ledgerPatch)
        {
            if (ledgerPatch is null)
            {
                return BadRequest("Ledger object is null.");
            }
            var result = await _service.LedgerService.GetLedgerForPatchAsync(id, trackChanges: true);
            ledgerPatch.ApplyTo(result.ledgerForUpdate);
            TryValidateModel(result.ledgerForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _service.LedgerService.SaveChangesForPatchAsync(result.ledgerForUpdate, result.ledgerEntity);

            return NoContent();
        }
    }
}
