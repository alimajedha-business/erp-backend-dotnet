using Accounting.Application.DTOs;
using Accounting.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Presentation.Controllers
{
    [Route("api/{companyId}/accounting/ledgers/{ledgerId}/accountsets")]
    [ApiController]
    public class AccountSetsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AccountSetsController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAccountSetsForCompany(int ledgerId)
        {
            var accountSets = await _service.AccountSetService.GetAllAsync(ledgerId, trackChanges: false);
            return Ok(accountSets);
        }
        [HttpGet("{id:int}", Name = "GetAccountSetForCompany")]
        public async Task<IActionResult> GetAccountSetForCompany(int ledgerId, int id)
        {
            var accountSet = await _service.AccountSetService.GetAsync(ledgerId, id,
            trackChanges: false);
            return Ok(accountSet);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(int companyId, int ledgerId, [FromBody] AccountSetForCreationDto accountSet)
        {
            if (accountSet is null)
                return BadRequest("AccountSet object is null");
            var AccounSetToReturn = await _service.AccountSetService
                .CreateAsync(companyId, ledgerId, accountSet, trackChanges: false);
            return CreatedAtRoute("GetAccountSetForCompany", new
            {
                companyId,
                ledgerId,
                id = AccounSetToReturn.Id
            },
            AccounSetToReturn);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccountSetForLedger(int companyId, int ledgerId, int id)
        {
           await _service.AccountSetService.DeleteAsync(companyId, ledgerId, id, trackChanges:
            false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAccountSetForLedger(int companyId, int ledgerId, int id, [FromBody] AccountSetForUpdateDto accountSet)
        {
            if (accountSet is null)
                return BadRequest("AccountSet object is null");
            await _service.AccountSetService.UpdateAsync(companyId, ledgerId, id, accountSet,
            ledTrackChanges: false, accTrackChanges: true);
            return NoContent();
        }
    }
}
