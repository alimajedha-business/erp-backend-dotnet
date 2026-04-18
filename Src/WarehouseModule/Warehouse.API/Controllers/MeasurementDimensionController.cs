using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/measurement-dimensions")]
public class MeasurementDimensionController(
    IMeasurementDimensionService dimensionService
) : ControllerBase
{
    private readonly IMeasurementDimensionService _dimensionService = dimensionService;

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] MeasurementDimensionParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _dimensionService.FilterByQAsync(parameters, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _dimensionService.GetByIdAsync(id, trackChanges: false, ct);
        return Ok(dto);
    }
}
