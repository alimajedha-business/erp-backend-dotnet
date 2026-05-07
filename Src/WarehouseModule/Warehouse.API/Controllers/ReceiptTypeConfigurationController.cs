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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt-type-configurations")]
public class ReceiptTypeConfigurationController(
    IReceiptTypeConfigurationService configurationService
) : ControllerBase
{
    private readonly IReceiptTypeConfigurationService _configurationService =
        configurationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateReceiptTypeConfigurationDto),
        typeof(CreateReceiptTypeConfigurationExample)
    )]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateReceiptTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.CreateAsync(companyId, createDto, ct);

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
        var dto = await _configurationService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpGet("by-receipt-type/{receiptTypeId:guid}")]
    public async Task<IActionResult> GetByReceiptTypeId(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.GetByReceiptTypeIdAsync(
            companyId,
            receiptTypeId,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _configurationService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }
}
