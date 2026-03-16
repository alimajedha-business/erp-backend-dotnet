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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/warehouse-locations")]
public class WarehouseLocationController(IWarehouseLocationService locationService) :
    ControllerBase
{
    private readonly IWarehouseLocationService _locationService = locationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateWarehouseLocationDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _locationService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _locationService.GetAllAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> GetAdvancedSearch(
        [FromQuery] WarehouseLocationParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _locationService.GetAllAsync(
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _locationService.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchWarehouseLocationDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _locationService.PatchAsync(id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _locationService.DeleteAsync(id, ct);
        return NoContent();
    }
}
