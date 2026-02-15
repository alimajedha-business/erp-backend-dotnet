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
        var dto = await _attributeService.CreateAttributeAsync(
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
        var attributes = await _attributeService.GetAllAttributesAsync(
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
        var dto = await _attributeService.GetAttributeByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("{id:guid}/enums")]
    [SkipModelValidation]
    public async Task<IActionResult> GetAttributeEnums(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromQuery] AttributeEnumValueParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var enumsDto = await _attributeEnumValueService
            .GetAttributeAllEnumValuesAsync(
                companyId,
                id,
                parameters,
                ct,
                filterNodeDto
            );

        return Ok(enumsDto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchAttributeDto> patchDoc,
        CancellationToken ct
    )
    {
        var dto = await _attributeService.PatchAttributeAsync(
            companyId,
            id,
            patchDoc,
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
        await _attributeService.DeleteAttributeAsync(companyId, id, ct);
        return NoContent();
    }
}
