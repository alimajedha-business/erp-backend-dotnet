using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipts")]
public class ReceiptController(
    IReceiptService receiptService
) : ControllerBase
{
    private readonly IReceiptService _receiptService = receiptService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(typeof(CreateReceiptDto), typeof(CreateReceiptExample))]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateReceiptDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _receiptService.CreateAsync(
            companyId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _receiptService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }
}
