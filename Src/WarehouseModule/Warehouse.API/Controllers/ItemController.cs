using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Domain.Exceptions;
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
        var items = await _itemService.GetItemsAsync(itemParameters);

        return Ok(new
        {
            success = true,
            data = items,
            count = items.Count()
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItemById(Guid id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item is not null)
        {
            return Ok(new
            {
                success = true,
                data = item,
            });
        }

        throw new ItemNotFoundException(id);
    }
}
