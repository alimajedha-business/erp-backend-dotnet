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
    ICategoryAttributeRuleService categoryAttributeRuleService,
    IItemService itemService
) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ICategoryAttributeRuleService _categoryAttributeRuleService = 
        categoryAttributeRuleService;

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
        var dto = await _categoryService.CreateCategoryAsync(
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
        [FromQuery] CategoryParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _categoryService.GetAllCategoriesAsync(
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
        var dto = await _categoryService.GetCategoryByIdAsync(
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
        await _categoryService.GetCategoryByIdAsync(
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
        var dto = await _categoryService.PatchCategoryAsync(
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
        await _categoryService.DeleteCategoryAsync(companyId, id, ct);
        return NoContent();
    }

    #region AttributeRules

    [HttpPost("{categoryId:guid}/attribute-rules")]
    public async Task<IActionResult> CreateAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromBody] CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _categoryAttributeRuleService
            .CreateCategoryAttributeRuleAsync(
                companyId,
                categoryId,
                createDto,
                ct
            );

        return CreatedAtAction(
            nameof(GetAttributeRuleById),
            new { companyId, categoryId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{categoryId:guid}/attribute-rules/list")]
    public async Task<IActionResult> GetAttributeRules(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromQuery] CategoryAttributeRuleParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _categoryAttributeRuleService
            .GetAllCategoryAttributeRulesAsync(
                companyId,
                categoryId,
                parameters,
                ct
            );

        return Ok(result);
    }

    [HttpGet("{categoryId:guid}/attribute-rules/{id:guid}")]
    public async Task<IActionResult> GetAttributeRuleById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _categoryAttributeRuleService
            .GetCategoryAttributeRuleByIdAsync(
                companyId,
                categoryId,
                id,
                ct
            );

        return Ok(dto);
    }

    [HttpPatch("{categoryId:guid}/attribute-rules/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _categoryAttributeRuleService
            .PatchCategoryAttributeRuleAsync(
                companyId,
                categoryId,
                id,
                patchDocument,
                ct
            );

        return Ok(dto);
    }

    [HttpDelete("{categoryId:guid}/attribute-rules/{id:guid}")]
    public async Task<IActionResult> DeleteAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _categoryAttributeRuleService
            .DeleteCategoryAttributeRuleAsync(
                companyId,
                categoryId,
                id,
                ct
            );

        return NoContent();
    }

    #endregion
}
