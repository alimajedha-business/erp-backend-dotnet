using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/warehouse-types")]
public class WarehouseTypeController(
    IWarehouseTypeService warehouseTypeService
) : ControllerBase
{
    private readonly IWarehouseTypeService _warehouseTypeService = warehouseTypeService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(typeof(CreateWarehouseTypeDto), typeof(WarehouseTypeCategoryExample))]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateWarehouseTypeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _warehouseTypeService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] WarehouseTypeParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _warehouseTypeService.GetAllAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseTypeAdvancedSearchExample))]
    public async Task<IActionResult> GetAdvancedSearch(
        [FromRoute] Guid companyId,
        [FromQuery] WarehouseTypeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _warehouseTypeService.GetAllAsync(
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
        var dto = await _warehouseTypeService.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var code = await _warehouseTypeService.GetNextCode(ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchWarehouseTypeDto>),
        typeof(WarehouseTypePatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchWarehouseTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _warehouseTypeService.PatchAsync(
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
        await _warehouseTypeService.DeleteAsync(id, ct);
        return NoContent();
    }
}
