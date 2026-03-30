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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/item-types")]
public class ItemTypeController(
    IItemTypeService itemTypeService
) : ControllerBase
{
    private readonly IItemTypeService _itemTypeService = itemTypeService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateItemTypeDto),
        typeof(CreateItemTypeExample)
    )]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateItemTypeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _itemTypeService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] ItemTypeParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _itemTypeService.GetAllAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(ItemTypeAdvancedSearchExample))]
    public async Task<IActionResult> Get(
        [FromQuery] ItemTypeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var attributes = await _itemTypeService.GetAllAsync(
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(attributes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _itemTypeService.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        CancellationToken ct
    )
    {
        var code = await _itemTypeService.GetNextCode(ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchItemTypeDto>),
        typeof(ItemTypePatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchItemTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _itemTypeService.PatchAsync(
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _itemTypeService.DeleteAsync(id, ct);
        return NoContent();
    }
}
