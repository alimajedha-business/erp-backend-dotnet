using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt-source-of-supplies")]
public class ReceiptSourceOfSupplyController(
    IReceiptSourceOfSupplyService receiptSourceOfSupplyService
) : ControllerBase
{
    private readonly IReceiptSourceOfSupplyService _receiptSourceOfSupplyService = receiptSourceOfSupplyService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateReceiptSourceOfSupplyDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _receiptSourceOfSupplyService.CreateAsync(companyId, createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] ReceiptSourceOfSupplyParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _receiptSourceOfSupplyService.FilterByQAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] ReceiptSourceOfSupplyParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _receiptSourceOfSupplyService.GetFilteredAsync(
            companyId, parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _receiptSourceOfSupplyService.GetByIdAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchReceiptSourceOfSupplyDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _receiptSourceOfSupplyService.PatchAsync(
            companyId, id,
            patchDocument,
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
        await _receiptSourceOfSupplyService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }
}
