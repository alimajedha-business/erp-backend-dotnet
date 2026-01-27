using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] CategoryParameters categoryParameters)
    {
        try
        {
            var categories = await _categoryService.GetCategoriesAsync(categoryParameters);

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
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return Ok(new
            {
                success = true,
                data = category,
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                error = "Failed to fetch Category with the given Id.",
                message = ex.Message
            });
        }
    }
}
