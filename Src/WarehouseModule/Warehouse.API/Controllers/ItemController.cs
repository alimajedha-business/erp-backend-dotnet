using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/items")]
public class ItemController(
    IItemService itemService
) : ControllerBase
{
    private readonly IItemService _itemService = itemService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateItemDto createItemDto,
        CancellationToken ct
    )
    {
        var itemDto = await _itemService.CreateItemAsync(
            companyId,
            createItemDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = itemDto.Id },
            itemDto
        );
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] ItemParameters itemParameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _itemService.GetAllItemsAsync(
            companyId,
            itemParameters,
            ct,
            filterNodeDto
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

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] UpdateItemDto updateItemDto,
        CancellationToken ct
    )
    {
        var itemDto = await _itemService.UpdateItemAsync(
            companyId,
            id,
            updateItemDto,
            ct
        );

        return Ok(itemDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _itemService.DeleteItemAsync(companyId, id, ct);
        return Ok();
    }
}
