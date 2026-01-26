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
[Route("api/v{version:apiVersion}/warehouse/items/")]
//[JwtAuthorize]
public class ItemController(
    IItemService itemService,
    ILogger<ItemController> logger,
    ICurrentUserService currentUserService
) : ControllerBase
{
    private readonly IItemService _itemService = itemService;
    private readonly ILogger<ItemController> _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] ItemParameters prms)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["Endpint"] = "GetItems",
            ["User"] = _currentUserService.Username ?? "Anonymous"
        }))
        {
            try
            {
                _logger.LogInformation("Fetching paginated list of Item for user {Username}.", _currentUserService.Username);

                var items = await _itemService.GetItemsAsync(prms);

                _logger.LogInformation("Retrieved {Count} items.", items.Count());

                return Ok(new
                {
                    success = true,
                    data = items,
                    count = items.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error fetching paginated list of Item for user {Username}.",
                    _currentUserService.Username
                );

                return StatusCode(500, new
                {
                    success = false,
                    error = "Failed to fetch Items",
                    message = ex.Message
                });
            }
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItemById(Guid id)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["Endpint"] = "GetItemById",
            ["User"] = _currentUserService.Username ?? "Anonymous"
        }))
        {
            try
            {
                _logger.LogInformation("Fetching Item with the given Id for user {Username}.", _currentUserService.Username);

                var item = await _itemService.GetItemByIdAsync(id);

                return Ok(new
                {
                    success = true,
                    data = item,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error fetching Item with the given Id for user {Username}.",
                    _currentUserService.Username
                );

                return StatusCode(500, new
                {
                    success = false,
                    error = "Failed to fetch Item with the given Id.",
                    message = ex.Message
                });
            }
        }
    }
}
