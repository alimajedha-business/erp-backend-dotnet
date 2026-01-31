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
[Route("api/v{version:apiVersion}/warehouse/categories/")]
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
        [FromBody] CreateCategoryDto createCategoryDto,
        CancellationToken ct
    )
    {
        var categoryDto = await _categoryService.CreateAsync(createCategoryDto, ct);
        return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] CategoryParameters categoryParameters)
    {
        var result = await _categoryService.GetListAsync(categoryParameters);
        return Ok(result);
    }

    [HttpPost("search/")]
    [SkipModelValidation]
    public async Task<IActionResult> GetListWithSearch(
        [FromQuery] CategoryParameters categoryParameters,
        [FromBody] FilterNodeDto? filterNodeDto
    )
    {
        var requestAdvancedFilters = _filterBuilder.Build<Category>(filterNodeDto);

        var categories = await _categoryService.GetListAsync(
            categoryParameters,
            requestAdvancedFilters
        );

        return Ok(new
        {
            Data = categories
        });
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        return Ok(new
        {
            success = true,
            data = category,
        });
    }

    [HttpPatch]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    )
    {
        var categoryDto = await _categoryService.UpdateAsync(id, updateCategoryDto, ct);
        return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var succeed = await _categoryService.DeleteAsync(id);

        return Ok(new
        {
            success = true,
        });
    }
}
