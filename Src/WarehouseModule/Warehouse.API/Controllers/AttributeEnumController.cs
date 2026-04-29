using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.DTOs;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var enumsDto = await _attributeEnumValueService.GetByIdAsync(
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
        var code = await _attributeEnumValueService.GetNextCode(attributeId, ct);
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
        await _attributeEnumValueService.DeleteAsync(attributeId, id, ct);
        return NoContent();
    }
}
