using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Presentation.Controllers
{
    internal class AccountSetController : ControllerBase
    {
        [Route("api/accounting/ledger/{lederId}/accountsets")]
        [ApiController]
        public class AccountSetsController : ControllerBase
        {
            private readonly IServiceManager _service;
            public AccountSetsController(IServiceManager service) => _service = service;

            [HttpGet]
            public IActionResult GetAccountSetsForCompany(int ledgerId)
            {
                var employees = _service.AccountSetService.GetAllAccountSets(ledgerId, trackChanges: false);
                return Ok(employees);
            }
        }
    }
}
