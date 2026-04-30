using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/warehouses/{warehouseId:guid}/locations")]
public class WarehouseLocationController(
    IWarehouseLocationService locationService
) : ControllerBase
{
    private readonly IWarehouseLocationService _locationService = locationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateWarehouseLocationDto),
        typeof(WarehouseLocationCreateRequestExample))
    ]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromRoute] Guid warehouseId,
        [FromBody] CreateWarehouseLocationDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _locationService.CreateAsync(
            warehouseId,
            createDto,
            ct
        );

        // FIX: warehouse is null in response (not in db)
        return CreatedAtAction(
            nameof(GetById),
            new { companyId, warehouseId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid warehouseId,
        [FromQuery] WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _locationService.FilterByQAsync(warehouseId, parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseLocationAdvancedSearchRequestExample))]
    [ProducesResponseType(typeof(ListResponseModel<WarehouseLocationListDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(WarehouseLocationGetListResponseExample))]
    public async Task<IActionResult> Get(
        [FromRoute] Guid warehouseId,
        [FromQuery] WarehouseLocationParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _locationService.GetFilteredAsync(
            warehouseId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid warehouseId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _locationService.GetByIdAsync(warehouseId, id, ct);
        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid warehouseId,
        CancellationToken ct
    )
    {
        var code = await _locationService.GetNextCodeAsync(warehouseId, ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchWarehouseLocationDto>),
        typeof(WarehouseLocationPatchRequestExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid warehouseId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchWarehouseLocationDto> patchDocument,
        CancellationToken ct
    )
    {
        // TODO: check if the location with the given warehouse does exist.
        var dto = await _locationService.PatchAsync(warehouseId, id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid warehouseId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _locationService.DeleteAsync(warehouseId, id, ct);
        return NoContent();
    }
}
