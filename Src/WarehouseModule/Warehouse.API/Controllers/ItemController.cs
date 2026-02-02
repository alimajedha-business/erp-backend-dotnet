using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/{companyId}/warehouse/items")]
public class ItemController(IItemService itemService) : ControllerBase
{
    private readonly IItemService _itemService = itemService;

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] ItemParameters itemParameters,
        CancellationToken ct
    )
    {
        var result = await _itemService.GetAllItemsAsync(
            companyId,
            itemParameters,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var item = await _itemService.GetItemByIdAsync(companyId, id, ct);
        return Ok(item);
    }
}
