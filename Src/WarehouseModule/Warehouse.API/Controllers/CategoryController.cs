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
[Route("api/v{version:apiVersion}/{companyId}/warehouse/categories/")]
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
        Guid companyId,
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
        Guid companyId,
        [FromQuery] CategoryParameters categoryParameters
    )
    {
        var result = await _categoryService.GetAllCategoriesAsync(
            companyId,
            categoryParameters
        );

        return Ok(result);
    }

    [HttpPost("search/")]
    [SkipModelValidation]
    public async Task<IActionResult> GetWithSearch(
        Guid companyId,
        [FromQuery] CategoryParameters categoryParameters,
        [FromBody] FilterNodeDto? filterNodeDto
    )
    {
        var requestAdvancedFilters = _filterBuilder.Build<Category>(filterNodeDto);

        var result = await _categoryService.GetAllCategoriesAsync(
            companyId,
            categoryParameters,
            requestAdvancedFilters
        );

        return Ok(result);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid companyId, Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(companyId, id);
        return Ok(category);
    }

    [HttpPatch]
    public async Task<IActionResult> Update(
        Guid companyId,
        Guid id,
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

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = categoryDto.Id },
            categoryDto
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid companyId, Guid id)
    {
        await _categoryService.DeleteCategoryAsync(companyId, id);
        return Ok();
    }
}
