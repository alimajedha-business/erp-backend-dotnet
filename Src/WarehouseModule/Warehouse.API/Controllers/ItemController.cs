using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/{companyId}/warehouse/items/")]
//[JwtAuthorize]
public class ItemController(IItemService itemService) : ControllerBase
{
    private readonly IItemService _itemService = itemService;

    [HttpGet]
    public async Task<IActionResult> GetPaginated(
        Guid companyId,
        [FromQuery] ItemParameters itemParameters
    )
    {
        var result = await _itemService.GetListAsync(companyId, itemParameters);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid companyId, Guid id)
    {
        var item = await _itemService.GetByIdAsync(companyId, id);
        return Ok(item);
    }
}
