using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/categories/")]
//[JwtAuthorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;
    private readonly ICurrentUserService _currentUserService;

    public CategoryController(
        ICategoryService categoryService,
        ILogger<CategoryController> logger,
        ICurrentUserService currentUserService
    )
    {
        _categoryService = categoryService;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCatogories([FromQuery] CategoryParameters prms)
    {
        using (_logger.BeginScope(new Dictionary<string, object> 
        {
            ["Endpint"] = "GetCategories",
            ["User"] = _currentUserService.Username ?? "Anonymous"
        }))
        {
            try
            {
                _logger.LogInformation("Fetching paginated list of Category for user {Username}.", _currentUserService.Username);

                var categories = await _categoryService.GetCategoriesAsync(prms);

                _logger.LogInformation("Retrieved {Count} categories.", categories.Count());

                return Ok(new
                {
                    success = true,
                    data = categories,
                    count = categories.Count()
                });
            }
            catch (Exception ex) 
            {
                _logger.LogError(
                    ex,
                    "Error fetching paginated list of Category for user {Username}.",
                    _currentUserService.Username
                );

                return StatusCode(500, new
                {
                    success = false,
                    error = "Failed to fetch Categories",
                    message = ex.Message
                });
            }
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["Endpint"] = "GetCategoryById",
            ["User"] = _currentUserService.Username ?? "Anonymous"
        }))
        {
            try
            {
                _logger.LogInformation("Fetching Category with the given Id for user {Username}.", _currentUserService.Username);

                var category = await _categoryService.GetCategoryByIdAsync(id);

                return Ok(new
                {
                    success = true,
                    data = category,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error fetching Category with the given Id for user {Username}.",
                    _currentUserService.Username
                );

                return StatusCode(500, new
                {
                    success = false,
                    error = "Failed to fetch Category with the given Id.",
                    message = ex.Message
                });
            }
        }
    }
}
