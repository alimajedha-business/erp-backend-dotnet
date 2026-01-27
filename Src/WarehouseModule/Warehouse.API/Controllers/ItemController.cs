using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/items/")]
//[JwtAuthorize]
public class ItemController(IItemService itemService) : ControllerBase
{
    private readonly IItemService _itemService = itemService;

    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] ItemParameters itemParameters)
    {
        try
        {
            var items = await _itemService.GetItemsAsync(itemParameters);

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
