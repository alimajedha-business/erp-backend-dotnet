using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using GeneralWarehouse = NGErp.General.Domain.Constants;

namespace NGErp.Warehouse.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/si-units")]
[HasPermission(GeneralWarehouse.EntityTypes.SiUnit)]
public class SiUnitController(
    ISiUnitService unitService
) : ControllerBase
{
    private readonly ISiUnitService _unitService = unitService;

    [HttpGet("filter-by-q")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromQuery] SiUnitParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _unitService.FilterByQAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromQuery] SiUnitParameters parameters,
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