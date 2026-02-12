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
    IItemService itemService
) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IItemService _itemService = itemService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateCategoryDto createCategoryDto,
        CancellationToken ct
    )
    {
        var categoryDto = await _categoryService.CreateCategoryAsync(
            companyId,
            createCategoryDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = categoryDto.Id },
            categoryDto
        );
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters categoryParameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _categoryService.GetAllCategoriesAsync(
            companyId,
            categoryParameters,
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
        var category = await _categoryService.GetCategoryByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(category);
    }

    [HttpPost("{id:guid}/items/list")]
    [SkipModelValidation]
    public async Task<IActionResult> GetItems(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromQuery] ItemParameters itemParameters,
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
            itemParameters,
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
        [FromBody] JsonPatchDocument<PatchCategoryDto> patchDoc,
        CancellationToken ct
    )
    {
        var categoryDto = await _categoryService.PatchCategoryAsync(
            companyId,
            id,
            patchDoc,
            ct
        );

        return Ok(categoryDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _categoryService.DeleteCategoryAsync(companyId, id, ct);
        return Ok();
    }
}
