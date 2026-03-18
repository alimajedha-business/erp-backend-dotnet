using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/inventory-movement-types")]
public class InventoryMovementTypeController(
    IInventoryMovementTypeService inventoryMovementTypeService
) : ControllerBase
{
    private readonly IInventoryMovementTypeService _inventoryMovementTypeService = inventoryMovementTypeService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateInventoryMovementTypeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _inventoryMovementTypeService.CreateAsync(
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

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
    [FromRoute] Guid companyId,
    [FromQuery] InventoryMovementTypeParameters parameters,
    CancellationToken ct
)
    {
        var result = await _inventoryMovementTypeService.GetAllAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] InventoryMovementTypeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _inventoryMovementTypeService.GetAllAsync(
            companyId,
            parameters,
            ct,
            filterNodeDto
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
        var warehouse = await _inventoryMovementTypeService.GetByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(warehouse);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var code = await _inventoryMovementTypeService.GetNextCode(companyId, ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchInventoryMovementTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _inventoryMovementTypeService.PatchAsync(
            companyId,
            id,
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
        await _inventoryMovementTypeService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }
}
