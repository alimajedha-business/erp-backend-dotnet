using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/{companyId}/warehouse/categories")]
//[JwtAuthorize]
public class CategoryController(
    ICategoryService categoryService,
    IAdvancedFilterBuilder filterBuilder
) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;

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

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters categoryParameters,
        CancellationToken ct
    )
    {
        var result = await _categoryService.GetAllCategoriesAsync(
            companyId,
            categoryParameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("search")]
    [SkipModelValidation]
    public async Task<IActionResult> GetWithSearch(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters categoryParameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var requestAdvancedFilters = _filterBuilder.Build<Category>(filterNodeDto);

        var result = await _categoryService.GetAllCategoriesAsync(
            companyId,
            categoryParameters,
            ct,
            requestAdvancedFilters
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

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    )
    {
        var categoryDto = await _categoryService.UpdateCategoryAsync(
            companyId,
            id,
            updateCategoryDto,
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
        return NoContent();
    }
}
