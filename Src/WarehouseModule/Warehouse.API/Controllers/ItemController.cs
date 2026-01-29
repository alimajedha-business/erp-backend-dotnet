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
    public async Task<IActionResult> GetPaginated([FromQuery] ItemParameters itemParameters)
    {
        var items = await _itemService.GetListAsync(itemParameters);

        return Ok(new
        {
            success = true,
            data = items,
            count = items.Count()
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _itemService.GetByIdAsync(id);

        return Ok(new
        {
            success = true,
            data = item,
        });
    }
}
