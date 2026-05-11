using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/units")]
public class UnitController(
    IUnitService unitService
) : ControllerBase
{
    private readonly IUnitService _unitService = unitService;

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] UnitParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _unitService.FilterByQAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromQuery] UnitParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _unitService.GetFilteredAsync(
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _unitService.GetByIdAsync(id, trackChanges: false, ct);
        return Ok(dto);
    }
}
