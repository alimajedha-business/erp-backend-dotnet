using Microsoft.AspNetCore.Mvc;
using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using System.Runtime.InteropServices;

namespace NGErp.Accounting.Presentation.Controllers
{
    [ApiController]
    [Route("api/accounting/[controller]")]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerService _service;

        public LedgerController(ILedgerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ledgers = await _service.GetAllAsync();
            return Ok(ledgers);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LedgerDTo dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }
    }
}