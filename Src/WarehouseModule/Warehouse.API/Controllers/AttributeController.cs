using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId}/warehouse/attributes")]
public class AttributeController(
    IAttributeService attributeService,
    IAttributeEnumValueService attributeEnumValueService,
    IAdvancedFilterBuilder filterBuilder
) : ControllerBase
{
    private readonly IAttributeService _attributeService = attributeService;
    private readonly IAttributeEnumValueService _attributeEnumValueService = attributeEnumValueService;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateAttributeDto createAttributeDto,
        CancellationToken ct
    )
    {
        var attributeDto = await _attributeService.CreateAttributeAsync(
            companyId,
            createAttributeDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = attributeDto.Id },
            attributeDto
        );
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] AttributeParameters attributeParameters,
        [FromBody] FilterRequest? filterRequest,
        CancellationToken ct
    )
    {
        var filterNodeDto = filterRequest?.Filter;
        var advancedFilters = _filterBuilder
            .Build<Domain.Entities.Attribute>(filterNodeDto);

        var attributes = await _attributeService.GetAllAttributesAsync(
            companyId,
            attributeParameters,
            ct,
            advancedFilters
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
        var attribute = await _attributeService.GetAttributeByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(attribute);
    }

    [HttpPost("{id:guid}/enums")]
    [SkipModelValidation]
    public async Task<IActionResult> GetAttributeEnums(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromQuery] AttributeEnumValueParameters attributeEnumParameters,
        [FromBody] FilterRequest? filterRequest,
        CancellationToken ct
    )
    {
        var filterNodeDto = filterRequest?.Filter;
        var advancedFilters = _filterBuilder
            .Build<AttributeEnumValue>(filterNodeDto);

        var attributeEnums = await _attributeEnumValueService
            .GetAttributeAllEnumValuesAsync(
                companyId,
                id,
                attributeEnumParameters,
                ct,
                advancedFilters
            );

        return Ok(attributeEnums);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _attributeService.DeleteAttributeAsync(companyId, id, ct);
        return Ok();
    }
}
