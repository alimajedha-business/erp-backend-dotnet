using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.Domain.Exceptions;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/categories/")]
//[JwtAuthorize]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<CategoryDto>> Create(
        [FromBody] CreateCategoryDto createCategoryDto,
        CancellationToken ct
    )
    {
        var categoryDto = await _categoryService.CreateAsync(createCategoryDto, ct);
        return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] CategoryParameters categoryParameters)
    {
        try
        {
            var categories = await _categoryService.GetPaginatedAsync(categoryParameters);

            return Ok(new
            {
                success = true,
                data = categories,
                count = categories.Count()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                error = "Failed to fetch Categories",
                message = ex.Message
            });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category is not null)
        {
            return Ok(new
            {
                success = true,
                data = category,
            });
        }

        throw new CategoryNotFoundException(id);
    }
}
