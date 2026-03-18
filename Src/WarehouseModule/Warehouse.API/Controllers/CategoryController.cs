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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/categories")]
public class CategoryController(
    ICategoryService categoryService,
    ICategoryAttributeRuleService attributeRuleService,
    IItemService itemService
) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ICategoryAttributeRuleService _attributeRuleService = attributeRuleService;
    private readonly IItemService _itemService = itemService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        if (createDto.ParentCategoryId is not null)
        {
            await _categoryService.GetByIdAsync(
                companyId,
                createDto.ParentCategoryId.Value,
                ct
            );
        }

        var dto = await _categoryService.CreateAsync(
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

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _categoryService.GetAllAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> GetAdvancedSearch(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _categoryService.GetAllAsync(
            companyId,
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
        var dto = await _categoryService.GetByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("{id:guid}/items/list")]
    [SkipModelValidation]
    public async Task<IActionResult> GetItems(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromQuery] ItemParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        await _categoryService.GetByIdAsync(
            companyId,
            id,
            ct
        );

        var result = await _itemService.GetCategoryAllItemsAsync(
            companyId,
            id,
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(result);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _categoryService.PatchAsync(
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
        await _categoryService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    #region AttributeRules

    [HttpPost("{categoryId:guid}/attribute-rules")]
    public async Task<IActionResult> CreateCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromBody] CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    )
    {
        await _categoryService.GetByIdAsync(companyId, categoryId, ct);
        var dto = await _attributeRuleService.CreateAsync(
            createDto,
            ct,
            e => e.CategoryId = categoryId
        );

        return CreatedAtAction(
            nameof(GetCategoryAttributeRuleById),
            new { companyId, categoryId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{categoryId:guid}/attribute-rules/list")]
    public async Task<IActionResult> GetCategoryAttributeRules(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromQuery] CategoryAttributeRuleParameters parameters,
        CancellationToken ct
    )
    {
        await _categoryService.GetByIdAsync(companyId, categoryId, ct);
        var result = await _attributeRuleService.GetAllAsync(
            categoryId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{categoryId:guid}/attribute-rules/{id:guid}")]
    public async Task<IActionResult> GetCategoryAttributeRuleById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _categoryService.GetByIdAsync(companyId, categoryId, ct);
        var dto = await _attributeRuleService.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpPatch("{categoryId:guid}/attribute-rules/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    )
    {
        await _categoryService.GetByIdAsync(companyId, categoryId, ct);
        var dto = await _attributeRuleService.PatchAsync(
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{categoryId:guid}/attribute-rules/{id:guid}")]
    public async Task<IActionResult> DeleteCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _categoryService.GetByIdAsync(companyId, categoryId, ct);
        await _attributeRuleService.DeleteAsync(id, ct);
        return NoContent();
    }

    #endregion
}
