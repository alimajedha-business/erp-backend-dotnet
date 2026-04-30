using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/categories/{categoryId:guid}/attribute-rules")]
public class CategoryAttributeRuleController(
    ICategoryAttributeRuleService attributeRuleService
) : ControllerBase
{
    private readonly ICategoryAttributeRuleService _attributeRuleService = attributeRuleService;

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromBody] CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _attributeRuleService.CreateAsync(
            companyId,
            categoryId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, categoryId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _attributeRuleService.GetByIdAsync(
            companyId,
            categoryId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _attributeRuleService.PatchAsync(
            companyId,
            categoryId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _attributeRuleService.DeleteAsync(companyId, categoryId, id, ct);
        return NoContent();
    }
}
