using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
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
        public IActionResult GetAccountSetsForCompany(int ledgerId)
        {
            var accountSets = _service.AccountSetService.GetAll(ledgerId, trackChanges: false);
            return Ok(accountSets);
        }
        [HttpGet("{id:int}", Name = "GetAccountSetForCompany")]
        public IActionResult GetAccountSetForCompany(int ledgerId, int id)
        {
            var accountSet = _service.AccountSetService.Get(ledgerId, id,
            trackChanges: false);
            return Ok(accountSet);
        }
        [HttpPost]
        public IActionResult CreateEmployeeForCompany(int companyId, int ledgerId, [FromBody] AccountSetForCreationDto accountSet)
        {
            if (accountSet is null)
                return BadRequest("AccountSet object is null");
            var AccounSetToReturn = _service.AccountSetService
                .Create(companyId, ledgerId, accountSet, trackChanges: false);
            return CreatedAtRoute("GetAccountSetForCompany", new
            {
                companyId,
                ledgerId,
                id = AccounSetToReturn.Id
            },
            AccounSetToReturn);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAccountSetForLedger(int companyId, int ledgerId, int id)
        {
            _service.AccountSetService.Delete(companyId, ledgerId, id, trackChanges:
            false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAccountSetForLedger(int companyId, int ledgerId, int id, [FromBody] AccountSetForUpdateDto accountSet)
        {
            if (accountSet is null)
                return BadRequest("AccountSet object is null");
            _service.AccountSetService.Update(companyId, ledgerId, id, accountSet,
            ledTrackChanges: false, accTrackChanges: true);
            return NoContent();
        }
    }
}
