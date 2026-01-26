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
        try
        {
            var items = await _itemService.GetItemsAsync(prms);

            return Ok(new
            {
                success = true,
                data = items,
                count = items.Count()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                error = "Failed to fetch Items",
                message = ex.Message
            });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItemById(Guid id)
    {
        try
        {
            var item = await _itemService.GetItemByIdAsync(id);

            return Ok(new
            {
                success = true,
                data = item,
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                error = "Failed to fetch Item with the given Id.",
                message = ex.Message
            });
        }
    }
}
