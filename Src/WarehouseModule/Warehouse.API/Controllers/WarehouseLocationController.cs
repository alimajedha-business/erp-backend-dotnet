using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/warehouse-locations")]
public class WarehouseLocationController(
    IWarehouseLocationService locationService
) : ControllerBase
{
    private readonly IWarehouseLocationService _locationService = locationService;

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> GetLocations(
        [FromRoute] Guid warehouseId,
        [FromQuery] WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _locationService.FilterByQAsync(
            warehouseId,
            parameters,
            ct
        );

        return Ok(result);
    }
}
