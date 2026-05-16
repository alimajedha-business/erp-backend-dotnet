using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/attributes/{attributeId:guid}/enums")]
public class AttributeEnumController(
    IAttributeEnumValueService attributeEnumValueService
) : ControllerBase
{
    private readonly IAttributeEnumValueService _attributeEnumValueService = attributeEnumValueService;

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromBody] CreateAttributeEnumValueDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _attributeEnumValueService.CreateAsync(
            companyId,
            attributeId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, attributeId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromQuery] AttributeEnumValueParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _attributeEnumValueService.FilterByQAsync(
            companyId,
            attributeId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromQuery] AttributeEnumValueParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var attributes = await _attributeEnumValueService.GetFilteredAsync(
            companyId,
            attributeId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(attributes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var enumsDto = await _attributeEnumValueService.GetByIdAsync(
            companyId,
            attributeId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(enumsDto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        CancellationToken ct
    )
    {
        var code = await _attributeEnumValueService.GetNextCode(
            companyId,
            attributeId,
            ct
        );

        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _attributeEnumValueService.PatchAsync(
            companyId,
            attributeId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _attributeEnumValueService.DeleteAsync(companyId, attributeId, id, ct);
        return NoContent();
    }
}
