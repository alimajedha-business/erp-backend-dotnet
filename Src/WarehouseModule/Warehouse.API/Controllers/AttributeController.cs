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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/attributes")]
public class AttributeController(
    IAttributeService attributeService,
    IAttributeEnumValueService attributeEnumValueService
) : ControllerBase
{
    private readonly IAttributeService _attributeService = attributeService;
    private readonly IAttributeEnumValueService _attributeEnumValueService = attributeEnumValueService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateAttributeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _attributeService.CreateAsync(
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

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] AttributeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var attributes = await _attributeService.GetAllAsync(
            companyId,
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(attributes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _attributeService.GetByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
    [FromRoute] Guid companyId,
    CancellationToken ct
)
    {
        var code = await _attributeService.GetNextCode(companyId, ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchAttributeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _attributeService.PatchAsync(
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
        await _attributeService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    #region EnumValue

    [HttpPost("{attributeId:guid}/enums")]
    public async Task<IActionResult> CreateAttributeEnumValue(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromBody] CreateAttributeEnumValueDto createDto,
        CancellationToken ct
    )
    {
        await _attributeService.GetByIdAsync(companyId, attributeId, ct);
        var dto = await _attributeEnumValueService.CreateAsync(
            createDto,
            ct,
            e => e.AttributeId = attributeId
        );

        return CreatedAtAction(
            nameof(GetAttributeEnumValueById),
            new { companyId, attributeId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{attributeId:guid}/enums/list")]
    public async Task<IActionResult> GetAttributeEnumValues(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromQuery] AttributeEnumValueParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        await _attributeService.GetByIdAsync(companyId, attributeId, ct);
        var enumsDto = await _attributeEnumValueService.GetAllAsync(
            attributeId,
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(enumsDto);
    }

    [HttpGet("{attributeId:guid}/enums/{id:guid}")]
    public async Task<IActionResult> GetAttributeEnumValueById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _attributeService.GetByIdAsync(companyId, attributeId, ct);
        var enumsDto = await _attributeEnumValueService.GetByIdAsync(id, ct);
        return Ok(enumsDto);
    }

    [HttpPatch("{attributeId:guid}/enums/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    )
    {
        await _attributeService.GetByIdAsync(companyId, attributeId, ct);
        var dto = await _attributeEnumValueService.PatchAsync(id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{attributeId:guid}/enums/{id:guid}")]
    public async Task<IActionResult> DeleteCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid attributeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _attributeService.GetByIdAsync(companyId, attributeId, ct);
        await _attributeEnumValueService.DeleteAsync(id, ct);
        return NoContent();
    }

    #endregion
}
